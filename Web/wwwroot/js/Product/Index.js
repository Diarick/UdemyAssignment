
var dataTable;
$(document).ready(function () {
    Table.BindingDataTable();
});

var Table = {
    BindingDataTable: function () {
        dataTable = $("#tblProduct").DataTable({
            "ajax": { url: '/product/getall' },
            "columns": [
                { data: 'title', "width": "10%" },
                { data: 'description', "width": "20%" },
                { data: 'isbn', "width": "10%" },
                { data: 'author', "width": "5%" },
                { data: 'category.name', "width": "10%" },
                { data: 'listPrice', "width": "10%" },
                { data: 'price', "width": "5%" },
                { data: 'price50', "width": "5%" },
                { data: 'price100', "width": "5%" },
                {
                    data: 'id',
                    "render": function (data) {
                        return '<div class="w-75 btn-group" role="group">'
                            + '<a href="/Product/Upsert?ProductId=' + data + '" class="btn btn-warning mx-2"><i class="bi bi-pencil-square">Edit</i></a>'
                            + '<a onClick=Transaction.DeleteProduct("/Product/Delete?id=' + data + '") class="btn btn-danger mx-2"><i class="bi bi-trash-fill">Delete</i></a>'
                            + '</div > ';
                    },
                    "width": '20%'
                }
            ]
        })
    }
}


var Transaction = {
    DeleteProduct: function (url) {
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: url,
                    type: "DELETE",
                    success: function (data) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                })
            }
        });
    }

}