// Add JavaScript evaluation section changes here.
//var s = "<%=Search(query)%>";
//document.write(s);


//$(document).ready(function () {
//    $("button").click(function () {
//        $("#searchUsers").load("123456789");
//        alert("data：");
//        $.post("/users/search",query,
//        function (data, status) {
//            alert("data：" + data + "\nstatus：" + status);
//        });
//    });
//});

function SearchUsers_answer(query) {
    $.ajax({
        type: "POST",
        url: "~/Controllers/UsersController.cs/Search",
        data: { query: $("#query").val() },
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        }
    });


    //alert("Starting searching");
    //$.ajax({
    //    url: "~/Controllers/UsersController.cs/Search",
    //    data: { query: $("#query").val() },
    //    success: function (result) {
    //        alert("result：" + result);

    //        // Original
    //        var select = $("#UserList")

    //        $.each(result, function (i) {
    //            var option = document.createElement('option');
    //            option.text = result[i].Text;
    //            option.value = result[i].Value;
    //            select.append(option);
    //        })
    //    }
    //});
}

function OnSuccess(response) {
    alert("Success!");
    alert(response.d);
}

// End changes.

// Solution:
function SearchUsers(query) {
    $.ajax({
        type: "POST",
        url: "~/Controllers/UsersController.cs/Search",
        data: { query: $("#query").val() },
        //contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        }
    });
}
// End solution.