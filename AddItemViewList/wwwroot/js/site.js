

var pageno = 1;
var pagesize = 5;
function GetItemList(fiPageno = pageno, fiPagesize=pagesize) {
    fsPageno = pageno;
    pagesize = $('#ddPageSize').val();
    fiPagesize = pagesize;
    var loData = new Object();
    loData.pageNo = fiPageno;
    loData.pageSize = fiPagesize;
    //getItemList('/Home/getItemList', 'GET', loData);
    $("#tblData tbody").empty();
    $.ajax({
        type: 'GET',
        url: '/Home/getItemList',
        dataType: 'text',
        data:  loData ,
        success: function (soutput) {
            //console.log("success");
            //console.log(soutput);
            $("#tblData tbody").append(soutput);
        },
        error: function (eoutput) {
            console.log("error");
            console.log(eoutput);
        }
    });
}

function nextPage() {
    var datacnt = $('#inCount').val();
    pagesize = $('#ddPageSize').val();
    pageno++;
    
    if (datacnt < (pageno * pagesize)) {
        pageno--;
        
    }

    GetItemList();
    console.log("Clicked Next page")
}

function previousPage() {
    pageno--;
    if (pageno <= 0) {
        pageno = 1;
    }
    GetItemList();
    console.log("Clicked Previous page")
}


