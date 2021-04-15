var dataTable;

$(document).ready(function () {
    loadTable();
});

console.log(SetId);
function loadTable() {
    console.log(SetId);
    dataTable = $("#table_id").DataTable
    (
            {
            "ajax": {
                "url": "/User/CreateSet/GetAll?SetId=" + SetId 
                },
                "columns":
                [
                    { "data": "nativeWord", "width": "30%" },
                    { "data": "learnWord", "width": "30%" }
              
                ]
            }
    );
}