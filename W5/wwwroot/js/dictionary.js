var dataTable;
var dataTable2;

$(document).ready(function () {
    loadTable();
    loadTable2();
});


function loadTable() {

    dataTable = $('#table_id').DataTable({
        "ajax": {
            "url": "/User/CreateSet/GetAll?SetId=" + SetId
        },
        "columns": [
            { "data": "nativeWord", "width": "15%" },
            { "data": "learnWord", "width": "15%" },
            { "data": "nativeSentence", "width": "15%" },
            { "data": "learnSentence", "width": "15%" },

            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/User/Dictionary/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer">
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
function loadTable2() {
    dataTable2 = $('#table2_id').DataTable({
        "ajax": {
            "url": "/User/Dictionary/GetAll?SetId=" + SetId
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "nativeLanguage", "width": "20%" },
            { "data": "learnLanguage", "width": "20%"},
            { "data": "createDate", "width": "20%" },
            
    
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a onclick=Delete("/User/Dictionary/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                 Delete  
                                </a>
                            </div>
                           `;
                }, "width": "20"
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
                        dataTable2.ajax.reload();
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


