var dataTable;

$(document).ready(function () {
    loadTable();
});


function loadTable() {
  
    dataTable = $('#table_id').DataTable({
        "ajax": {
            "url": "/User/CreateSet/GetAll?SetId=" + SetId 
        },
        "columns": [
            { "data": "nativeWord", "width": "25%" },
            { "data": "learnWord", "width": "25%" },
            
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/User/CreateSet/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                 Edit   
                                </a>
                                <a onclick=Delete("/User/CreateSet/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                 Delete  
                                </a>
                            </div>
                           `;
                }, "width": "25%"
            }
        ]
    });

}

function Delete(url) {
    console.log(url);
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}