var dataTable;

$(document).ready(function () {
    loadTable();
});

function loadTable() {

    dataTable = $("#table_id").DataTable
    (
        {
            "ajax": {
                "url": "/Admin/Users/GetAll"
            },
            "columns": [
                {"data": "userName", "width": "30%"},
                { "data": "email", "width": "30%" },
                {
                    "data": { id: "id", lockoutEnd: "lockoutEnd" },
                    "render": function (data) {
                        var today = new Date().getTime();
                        var lockout = new Date(data.lockoutEnd).getTime();
                        if (lockout > today) {
                            return `
                            <div class="text-center">
                                <a onclick=LockUnlock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:100px; width:100px;">
                                    <i class="fas fa-acorn"></i>  Unlock

                                </a>
                            </div>
                           `;
                        }
                        else {
                            return `
                            <div class="text-center">
                                <a onclick=LockUnlock('${data.id}') class="btn btn-success text-white" style="cursor:pointer" width:100px;>
                                    <i class="fas fa-acorn"></i> Lock
                                </a>
                            </div>
                           `;

                        }


                    }, "width": "50%"
                }

            ]
        }
    );
}

function LockUnlock(id) {

    $.ajax({
        type: "POST",
        url: '/Admin/Users/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
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