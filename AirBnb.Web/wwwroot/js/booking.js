﻿var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    const urlParams = new URLSearchParams(window.location.search);
    const status = urlParams.get('status');
    console.log(status);
    loadDataTable(status);

});

function loadDataTable(status) {
    dataTable = $('#tblVlla').DataTable({
        "ajax": { url: '/booking/getall?status=' + status },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'user.name', "width": "15%" },
            { data: 'phone', "width": "10%" },
            { data: 'email', "width": "15%" },
            { data: 'status', "width": "5%" },
            { data: 'checkInDate', "width": "7%" },
            { data: 'nights', "width": "5%" },
            { data: 'totalCost', render: $.fn.dataTable.render.number(',', '.', 2), "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/booking/bookingDetails?bookingId=${data}" class="btn btn-outline-warning mx-2"> <i class="bi bi-pencil-square"></i> Details</a>               
                    </div>`
                },
                "width": "15%"
            }
        ]
    });
}
