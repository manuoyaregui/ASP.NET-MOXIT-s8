
$(document).ready(function () {



    var url = "/api/movies";
    var dataTable = $("#movies").DataTable({
        ajax: {
            url: url,
            dataSrc: ""
        },
        columns: [
            {
                data: "name",
                render: (data, type, movie) => {
                    return "<a href='movie/edit/" + movie.id + "'>" + movie.name + "</a>";
                }
            },
            {
                data: "genre.name"
            },
            {
                data: "id",
                render: (data) => {

                    return "<button data-movieId= " + data + " class='btn btn-primary js-delete'>Delete</button>";
                }
            }


        ]
    });

    $("#movies").on("click", ".js-delete", function () {


        var button = $(this);
        var movieId = button.attr("data-movieId");


        bootbox.confirm("You sure you wanna do that? " + movieId, (result) => {

            if (result) {
                $.ajax({
                    url: url + "/" + movieId,
                    method: "DELETE",
                    success: function () {
                        dataTable.row(button.parents("tr")).remove().draw();
                        button.parents("tr").remove();
                    }
                });
            }
        });

    });


});