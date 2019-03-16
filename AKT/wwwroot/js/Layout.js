$(window).resize(function matchHeight() {
  windowHeight = $(window).height();
  document.getElementById('background').setAttribute("style", " min-height:" + windowHeight + "px;");
});

$(document).ready(function matchHeight() {
  windowHeight = $(window).height();
  document.getElementById('background').setAttribute("style", " min-height:" + windowHeight + "px;");
});