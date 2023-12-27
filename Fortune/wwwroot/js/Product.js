

var datatable;


function loadData(tableId, apiUrl, columns) {
    datatable = $(`#${tableId}`).DataTable({
        ajax: {
            url: apiUrl
        },
        columns: columns
    });
}


var ProductsColumns = [
    { data: 'title', "width": "15%" },
    { data: 'isbn', "width": "15%" },
    { data: 'author', "width": "15%" },
    { data: 'listPrice', "width": "15%" },
    { data: 'category.name', "width": "15%" },
    {
        data: 'id',
        "render": function (data) {
            return `<div class="w-75 btn-group" role="group">
                    <a href="/Products/Upsert?id=${data}" title="Edit" class="btn btn-primary bi-pencil-square mx-2">Edit</a>
                    <a onClick=Delete('/Products/delete?id=${data}') title="Delete" class="btn btn-danger bi bi-trash mx-2">Delete</a>
                    </div>`
        },
        "width": "20%"
    }
];


var CategoryColumns = [
    { data: 'name', "width": "30%" },
    { data: 'displayNumber', "width": "20%" }, {
        data: 'id',
        "render": function (data) {
            return `<div class="w-75 btn-group" role="group">
            <a href="/Category/Edit?id=${data}" title="Edit" class="btn btn-primary bi-pencil-square mx-2"> Edit</a>
            <a onClick=Delete('/Category/delete?id=${data}') title="Delete" class="btn btn-danger bi bi-trash mx-2">delete</a>
            </div>`
        }
    }
]


function Delete(url) {
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
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        datatable.ajax.reload();
                        Swal.fire({
                            title: "Deleted!",
                            text: "Your file has been deleted.",
                            icon: "success"
                        });
                    }
                    else {
                        datatable.ajax.reload();
                        Swal.fire({
                            title: "Error!",
                            text: data.message || "An error occurred while deleting.",
                            icon: "error"
                        });
                    }
                }
            })           
        }
    });
}

