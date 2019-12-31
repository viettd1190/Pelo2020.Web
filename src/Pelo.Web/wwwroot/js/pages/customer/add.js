$(document).ready(function () {
    addActiveClass('mnuCustomer');
    loadProvinces();
});
function  loadProvinces() {
    $.post('/Customer/GetAllProvinces',
        '',
        function(data) {
            if(data) {
                for(var i=0;i<data.length;i++) {
                    $('#ProvinceId')
                        .append('<option value="'+data[i].id+'">'+data[i].type+' '+data[i].name+'</option>');
                }
            }
        });
}
function loadDistricts() {
    var provinceId = $('#ProvinceId').val();
    $('#DistrictId').html('<option value="0">--Chọn quận/huyện--</option>');
    $('#WardId').html('<option value="0">--Chọn phường/xã--</option>');
    if (provinceId) {
        $.post('/Customer/GetAllDistricts',
            { id: provinceId },
            function (data) {
                if (data) {
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            $('#DistrictId')
                                .append('<option value="' + data[i].id + '">' + data[i].type + ' ' + data[i].name + '</option>');
                        }
                    }
                }
            });
    }
}
function loadWards() {
    var districtId = $('#DistrictId').val();
    $('#WardId').html('<option value="0">--Chọn phường/xã--</option>');
    if (districtId) {
        $.post('/Customer/GetAllWards',
            { id: districtId },
            function (data) {
                if (data) {
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            $('#WardId')
                                .append('<option value="' + data[i].id + '">' + data[i].type + ' ' + data[i].name + '</option>');
                        }
                    }
                }
            });
    }
}