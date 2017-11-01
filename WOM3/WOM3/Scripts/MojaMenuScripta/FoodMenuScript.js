
    var Cena = 0;

    $(document).ready(function () {

        $('#myTable').DataTable({

            "processing": true,
            "serverSide": true,
            "orderMulti": false,
            "lengthMenu": "Display _MENU_ records per page",



            "aLengthMenu": [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]],

            "oLanguage": {
                "sLengthMenu": "Prikazi _MENU_ Vrsta",
                "sSearch": "<span class='glyphicon glyphicon-search'></span>",
                "sInfo": "Prikazano izmedju _START_ i _END_ vrsta od _TOTAL_ ukupno",
                "oPaginate": {
                    "sPrevious": "Predhodna",
                    "sNext": "Sledeca"
                }

            },




            "ajax": {
                "url": 'http://localhost:5611/Home/LoadContactData',
                "type": "POST",
                "datatype": "json",
                "data": function (d) {
                    d.cr = $('#cr option:selected').val();
                    if (d.cr === undefined) {
                        d.cr = 1;
                    }
                }
            },


            "columns": [
                {
                    'render': function (data, type, row) {
                        return "<img class='slicka' src='http://localhost:5611/Images/" + row.Image + ".png' style='width:40px;height:40px;margin-right:2%'  /><span style='margin-left:1%'>" + row.Image + "</span>";

                    }
                },
              

                { "data": "Demage", "name": "Demage", "autoWidth": true },
                { "data": "Mana", "name": "Mana", "autoWidth": true },
                { "data": "Armor", "name": "Armor", "autoWidth": true },
                { "data": "Price", "name": "Price", "autoWidth": true }

            ],



        });
        $('#myTable_filter').append("&nbsp;&nbsp;<select style='margin:auto' id='cr' onchange='promeni()'><option value='100'>Izaberi Kriterijum</option><option value='0'>Naziv</option><option value='1'>Kategorija</option><option value='3'>Gramaza</option></select>");
        $('#myTable_filter').addClass('searchbox_1');
        $('#myTable_filter input').addClass('search_1');
        $('#myTable_filter input').attr("placeholder", "Pretrazi");
        $('#myTable_length').addClass('searchbox_1');
        $('#myTable_length select').addClass('zaDuzinu');
        $('#myTable_length label').addClass('boja');
        $('#myTable_info').addClass('fonts');
        $('#myTable_paginate').addClass('fonts');

    });

    var promeni = function () {
        $('#myTable').DataTable().draw();
    }








    $(document).ready(function () {

        $('#myTable tbody').on('click', 'tr', function () {

            var data = $("#myTable").DataTable().row(this).data();

            $("#myModal .naziv").text(data.Naziv);
            $("#myModal .kategorija").text(data.Kategorija);
            $("#myModal .kolicina").text(data.Kolicina);
            $("#myModal .gramaza").text(data.Gramaza);
            $("#myModal .opis").text(data.Opis);
            $("#myModal .cena").text(data.Cena);
            $("#myModal .slika").attr('src', 'http://localhost:64345/Images/' + data.Slika);
            $("#myModal input[type=hidden]").remove();
            $("#myModal").append("<input type='hidden' value='" + data.Id + "' />");

            //$("#myModal .rating").empty();
            //$("#myModal .rating").append(

            //    "<input type='radio' id='star5_2' name='rating2' value='5' /> <label class='full' for='star5_2' title='Awesome - 5 stars'></label>"+
            //   " <input type='radio' id='star4half_2' name='rating2' value='4 and a half' /> <label class='half' for='star4half_2' title='Pretty good - 4.5 stars'></label>"+
            //   " <input type='radio' id='star4_2' name='rating2' value='4' /> <label class='full' for='star4_2' title='Pretty good - 4 stars'></label>"+
            //   " <input type='radio' id='star3half_2' name='rating2' value='3 and a half' /> <label class='half' for='star3half_2' title='Meh - 3.5 stars'></label>"+
            //   " <input type='radio' id='star3_2' name='rating2' value='3' /> <label class='full' for='star3_2' title='Meh - 3 stars'></label>"+
            //   " <input type='radio' id='star2half_2' name='rating2' value='2 and a half' /> <label class='half' for='star2half_2' title='Kinda bad - 2.5 stars'></label>"+
            //   " <input type='radio' id='star2_2' name='rating2' value='2' /> <label class='full' for='star2_2' title='Kinda bad - 2 stars'></label>"+
            //   " <input type='radio' id='star1half_2' name='rating2' value='1 and a half' /> <label class='half' for='star1half_2' title='Meh - 1.5 stars'></label>"+
            //   " <input type='radio' id='star1_2' name='rating2' value='1' /> <label class='full' for='star1_2' title='Sucks big time - 1 star'></label>"+
            //   " <input type='radio' id='starhalf_2' name='rating2' value='half' /> <label class='half' for='starhalf_2' title='Sucks big time - 0.5 stars'></label>"
            //    );

            $("#myModal").modal('show');

        });

    });