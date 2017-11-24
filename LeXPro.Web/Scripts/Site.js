function PrintPreview(div_id) {
    var prtContent = document.getElementById(div_id);
    var win_print = window.open('', '', 'letf=0,top=0,width=800,height=800,toolbar=0,scrollbars=0,status=0');
    win_print.document.write('<html><head>');
    win_print.document.write('<style>  @page { margin: 0; } * {font-family: Roboto; font-size: 11px; margin: 0 px 5 px 0 px 5 px; } </style>');
    win_print.document.write('</head><body><br><br>');
    win_print.document.write(prtContent.innerHTML);
    var navi = win_print.document.getElementById('navibuttons');
    if (navi != null) navi.style.visibility = 'hidden';
    var btnback = win_print.document.getElementById('divbtnback');
    if (btnback != null) btnback.style.visibility = 'hidden';
    win_print.document.write('</body></html>');
    win_print.document.close();
    win_print.focus();
    win_print.print();
    win_print.close();
    prtContent.innerHTML = strOldOne;
}