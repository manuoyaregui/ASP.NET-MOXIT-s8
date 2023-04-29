
$(document).ready(function () {



    var url = "/api/customers";
    var dataTable = $("#customers").DataTable({
        ajax: {
            url: url,
            dataSrc: ""
        },
        columns: [
            {
                data: "name",
                render: (data, type, customer) => {
                    return "<a href='customers/edit/" + customer.id + "'>" + customer.name + "</a>";
                }
            },
            {
                data: "membershipType.name"
            },
            {
                data: "membershipType.discountRate",
                render: (data) => {
                    return data + "%";
                }
            },
            {
                data: "id",
                render: (data) => {

                    return "<button data-customerId= " + data + " class='btn btn-primary js-delete'>Delete</button>";
                }
            }


        ]
    });

    $("#customers").on("click",".js-delete", function() {


        var button = $(this);
        var customerId = button.attr("data-customerId");


        bootbox.confirm("You sure you wanna do that? " + customerId, (result) => {
            
            if (result) {
                $.ajax({
                    url: url + "/" + customerId,
                    method: "DELETE",
                    success: function() {
                        dataTable.row(button.parents("tr")).remove().draw();
                        button.parents("tr").remove();
                    }
                });
            }
        });

    });


});