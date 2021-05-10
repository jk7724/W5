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
                                 Edytuj
                                </a>
                                <a onclick=Delete("/User/CreateSet/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                 Usuń  
                                </a>
                            </div>
                           `;
                }, "width": "25%"
            }
        ]
    });

}

function Delete(url) {
    swal({
        title: "Ta operacja spowoduje usunięcie danych.",
        text: "Kliknij ok by przejść dalej.",
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