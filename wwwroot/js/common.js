reload_chart();

function reload_chart() {
    $(function () {
        $.ajax({
            async: true,
            type: "Post",
            url: "/Home/LoadDirections",
            dataType: "json",

            success: function (json) {
                //Bind Data Function
                createJSTrees(json.jsonvar);
                //For Refresh Chart
                window.$('#jstree').jstree(true).refresh();
            }
        });
    });
    
    function createJSTrees(jsonparams) {
        window.$('#jstree').on('changed.jstree', function (e, data) {
            var i, j, r = [];
            for (i = 0, j = data.selected.length; i < j; i++) {
                r.push(+data.instance.get_node(data.selected[i]).id);
            }
            if (r[0]) {
                document.querySelector('#linkEditDirection').setAttribute('href', 'directions/edit/' + r[0]);
                document.querySelector('#linkDeleteDirection').setAttribute('href', 'directions/delete/' + r[0]);
                
            } else {
                document.querySelector('#linkEditDirection').setAttribute('href', '#');
                document.querySelector('#linkDeleteDirection').setAttribute('href', '#');
               
            }
            $.ajax({
                type: "Post",
                data: { ids: r.join(',') },
                url: 'Workers/List',
            }).done(function (msg) {
                window.$("#gridContainer").html(msg);
            });
        }).jstree({
            "core": {
                "themes": {
                    "variant": "large"
                },
                "data": JSON.parse(jsonparams),
            },
            "checkbox": {
                "keep_selected_style": false
            },
        });

    }

    $("#addBtnDirection").click(function () {
        $.ajax({
            url: $(this).attr("formaction"),
        }).done(function(msg) {
            window.$("#AddContent").html(msg);
            window.$("#add-direction").modal();
        });
    });
    $("body").on("click", "#saveDirection", function () {
        var form = $('#directionForm');
        $.ajax({
            type: "post",
            url: form.attr('action'),
            data: {
               
                direction: {
                    Name: $("#Name").val()
                }
            },
            dataType: "html",
            success: function (result) {
                $("#add-direction").modal("hide");
                $("#partial").html(result);
            }
        });
        return false;
    });
}