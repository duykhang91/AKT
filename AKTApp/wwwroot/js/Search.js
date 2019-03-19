$(document.body).on('click', '.paging', function () {
  var value = $(this).attr("value");
  var name = $(this).attr("name");
  $('<input />').attr('type', 'hidden')
    .attr('name', name)
    .attr('value', value)
    .attr('id', "pageValue")
    .appendTo('#searchForm');

  $("#searchForm").submit();
});

$(document.body).on('click', '.update', function () {
  var value = $(this).attr("value");
  var name = $(this).attr("name");
  var id = value.replace("update", "#");
  id = id.replace("delete", "#");
  id = id.substr(0, id.indexOf("#"));

  document.getElementById("updateCell" + id).innerHTML = "<div uk-spinner></div>";

  $('<input />').attr('type', 'hidden')
    .attr('name', name)
    .attr('value', value)
    .attr('id', "updateValue")
    .appendTo('#updateForm');

  $("#updateForm").submit();
});

function disableAddButton() {
  document.getElementById("addButton").disabled = true;
  document.getElementById("addButton").innerHTML = "<div uk-spinner></div>";
}

showResult = function showResult() {
  $(document).ready(function () {
    $('#openmodal').trigger('click');
    $('#pageValue').remove();
    $('#updateValue').remove();
    document.getElementById("addButton").disabled = false;
    document.getElementById("addButton").innerHTML = "Add";
  });
};