
"use strict";

function dynAddRow($tableSelector)
{
    //alert("add-row click3");
   
    this.parentTable = $tableSelector;
    this.tempate = $(this.parentTable).data('template');
   // alert(this.tempate);
    this.evt = this.parentTable + '.add-row';
  
    this.addRow = function (e) {
       // alert("add-row click");
        if (e) { e.preventDefault(); }
        var $temp = $('#' + self.tempate).val();
        if ($temp == undefined) { return;}
        var totalRow = $(self.parentTable + ' tbody tr').length;
        $temp = $temp.replace(/{i}/g, totalRow);

        var $table = $(self.parentTable + ' tbody').append('<tr>' + $temp + '</tr>');

        applySelect();

        //focus on first input
        console.log($(self.parentTable + ' tbody tr:last-child').find('.search-select').eq(0));
        $(self.parentTable + ' tbody tr:last-child').find('.search-select').eq(0).focus();


    };
    this.removeRow = function (e) {
        if (e) { e.preventDefault(); }
        $(this).closest("tr").remove(); // remove row
        return false;
    };

    
    var self = this;

    $(document).on('click', self.parentTable + " .add-row", self.addRow);
    $(document).on('click', self.parentTable + ' .remove-row', self.removeRow);

    $("body").on("click", "#tblFloor .Edit", function () {
        var row = $(this).closest("tr");
        $("td", row).each(function () {
            if ($(this).find("input").length > 0) {
                $(this).find("#floorSelect").show();
                $(this).find("input:not(.hide)").show();
                $(this).find("span").hide();
            }
        });
        row.find(".Update").show();
        row.find(".Cancel").show();
        row.find(".Delete").hide();
        $(this).hide();
    });

    $("body").on("click", "#tblFloor .Update", function () {
        var row = $(this).closest("tr");
        $("td", row).each(function () {
            if ($(this).find("input").length > 0) {
                var span = $(this).find("span");
                if ($(this).find("#floorSelect").length > 0) {
                    var input = $(this).find("#floorSelect");
                }
                else {
                    var input = $(this).find("input");
                }
                span.html(input.val());
                span.show();
                input.hide();
            }
        });
        row.find(".Edit").show();
        row.find(".Delete").show();
        row.find(".Cancel").hide();
        $(this).hide();

        var floor = {};
        floor.floor_id = row.find(".FloorId").find("span").html();
        floor.floor_name = row.find(".FloorName").find("span").html();
        floor.area = row.find(".Area").find("span").html();
        floor.rate = row.find(".Rate").find("span").html();
        $.ajax({
            type: "POST",
            url: "/Dynamic/UpdateFloor",
            data: '{floor:' + JSON.stringify(floor) + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    });

    //Cancel event handler.
    $("body").on("click", "#tblFloor .Cancel", function () {
        var row = $(this).closest("tr");
        $("td", row).each(function () {
            if ($(this).find("input").length > 0) {
                var span = $(this).find("span");
                if ($(this).find("#floorSelect").length > 0) {
                    var input = $(this).find("#floorSelect");
                }
                else {
                    var input = $(this).find("input");
                }
                input.val(span.html());
                span.show();
                input.hide();
            }
        });
        row.find(".Edit").show();
        row.find(".Delete").show();
        row.find(".Update").hide();
        $(this).hide();
    });

    //Delete event handler.
    $("body").on("click", "#tblFloor .Delete", function () {
        if (confirm("Do you want to delete this row?")) {
            var row = $(this).closest("tr");
            var floor_id = row.find("span").html();
            $.ajax({
                type: "POST",
                url: "/Dynamic/DeleteFloor",
                data: '{floor_id: ' + floor_id + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if ($("#tblFloor tr").length > 2) {
                        row.remove();
                    } else {
                        row.find(".Edit").hide();
                        row.find(".Delete").hide();
                        row.find("span").html('&nbsp;');
                    }
                }
            });
        }
    });



}



