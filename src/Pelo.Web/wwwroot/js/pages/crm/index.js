var table = null;
$(document).ready(function () {
    addActiveClass('mnuCrm');
    $('.date-picker').flatpickr({
            enableTime:false,
            nextArrow:'<i class="zmdi zmdi-long-arrow-right" />',
            prevArrow:'<i class="zmdi zmdi-long-arrow-left" />'
        });
    loadProvinces();
    initTable();
    $('#txtCode').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    $('#txtCustomerCode').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    $('#txtCustomerName').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    $('#txtCustomerPhone').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    $('#txtCustomerAddress').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    $('#txtNeed').keyup(function (key) {
        if (key.keyCode === 13) {
            reloadData();
        }
    });
    reloadData();
});
function loadProvinces() {
    $.post('/Crm/GetAllProvinces',
        '',
        function(data) {
            if(data) {
                if(data.length>0) {
                    for(var i=0;i<data.length;i++) {
                        $('#txtProvince')
                            .append('<option value="'+data[i].id+'">'+data[i].type+' '+data[i].name+'</option>');
                    }
                }
            }
        });
}
function loadDistricts() {
    var provinceId=$('#txtProvince').val();
    $('#txtDistrict').html('<option value="0">--Tất cả quận/huyện--</option>');
    $('#txtWard').html('<option value="0">--Tất cả phường/xã--</option>');
    if(provinceId) {
        $.post('/Crm/GetAllDistricts',
            {id:provinceId},
            function(data) {
                if(data) {
                    if(data.length>0) {
                        for(var i=0;i<data.length;i++) {
                            $('#txtDistrict')
                                .append('<option value="'+data[i].id+'">'+data[i].type+' '+data[i].name+'</option>');
                        }
                    }
                }
            });
    }
}
function loadWards() {
    var districtId=$('#txtDistrict').val();
    $('#txtWard').html('<option value="0">--Tất cả phường/xã--</option>');
    if(districtId) {
        $.post('/Crm/GetAllWards',
            {id:districtId},
            function(data) {
                if(data) {
                    if(data.length>0) {
                        for(var i=0;i<data.length;i++) {
                            $('#txtWard')
                                .append('<option value="'+data[i].id+'">'+data[i].type+' '+data[i].name+'</option>');
                        }
                    }
                }
            });
    }
}
function initTable() {
    table = $('#tblCrms').DataTable({
        deferLoading: true,
        processing: true,
        serverSide: true,
        ordering: true,
        paging: true,
        searching: true,
        ajax: {
            url: "/Crm/GetList",
            method: "POST"
        },
        columns: [
            { data: "id" },
            { data: "crm_status" },
            { data: "code", sortable: false },
            { data: "customer_name", sortable: false },
            { data: "need", sortable: false },
            { data: "crm_priority" },
            { data: "user_created" },
            { data: "contact_date" },
            { data: "date_created" },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false },
            { data: "date_created", visible: false }
        ],
        columnDefs: [
            {
                targets: 0,
                data: 'id',
                orderable: false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            }, {
                targets: 1,
                data: 'crm_status',
                orderable: true,
                render: function (data, type, row, meta) {
                    return '<div style="margin:auto;"><span class="badge" style="color:#FFF; background-color: '+
                        row.crm_status_color+
                        '">'+
                        data+
                        '</span></div>';
                }
            }, {
                targets: 2,
                data: 'code',
                className: 'datatable-cell-center',
                searchable: true,
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="padding:10px;"><div style="padding-top:10px;"><a href="/Customer/Detail?id=' + row.customer_id +'&nextAction=Crm" style="font-weight:800;">' + row.code + '</a></div></div>';
                }
            }, {
                targets: 3,
                data: 'customer_name',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var customerName = '<span style="font-weight:600;font-size:16px;">' + data + '</span><br/>';
                    var phone = '<span><a href="tel:' + row.customer_phone + '">' + row.customer_phone + '</a></span><br/>';
                    var phone2 = '';
                    if (isNull(row.customer_phone_2)) {
                        phone2 = '<span><a href="tel:' + row.customer_phone_2 + '">' + row.customer_phone_2 + '</a></span><br/>';
                    }
                    var phone3 = '';
                    if (isNull(row.customer_phone_3)) {
                        phone3 = '<span><a href="tel:' + row.customer_phone_3 + '">' + row.customer_phone_3 + '</a></span><br/>';
                    }
                    var address = '';
                    var condition='';
                    if(isNull(row.province)) {
                        address = address + condition + row.province;
                        condition=', ';
                    }
                    if (isNull(row.district)) {
                        address = address + condition + row.district;
                        condition=', ';
                    }
                    if (isNull(row.ward)) {
                        address = address + condition + row.ward;
                        condition = ', ';
                    }
                    if(isNull(row.customer_address)) {
                        address = address + condition + row.customer_address;
                    }
                    if(isNull(address)) {
                        address = '<span>Địa chỉ: <span style="font-weight:700;">' + address + '</span></span><br/>';
                    }
                    var customerGroup='';
                    if(isNull(row.customer_group)) {
                        customerGroup='<span>Nhóm khách hàng: <span style="font-weight:700;">'+
                            row.customer_group+
                            '</span></span><br/>';
                    }
                    var customerVip = '';
                    if(isNull(row.customer_vip)) {
                        customerVip='<span>Mức độ thân thiết: <span style="font-weight:700;">'+
                            row.customer_vip+
                            '</span></span><br/>';
                    }
                    return '<div style="padding:10px;">'+
                        customerName+
                        phone+
                        phone2+
                        phone3+
                        address+
                        customerGroup+
                        customerVip+
                        '</div>';
                }
            }, {
                targets: 4,
                data: 'need',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var need = '';
                    if(isNull(row.need)) {
                        need = '<span>Nhu cầu: <span style="font-weight:700;">' + row.need + '</span></span><br/>';
                    }
                    var description = '';
                    if(isNull(row.description)) {
                        description='<span>Ghi chú: <span style="font-weight:700;">'+
                            row.description+
                            '</span></span><br/>';
                    }
                    var productGroup = '';
                    if(isNull(row.product_group)) {
                        productGroup='<span>Nhóm sản phẩm: <span style="font-weight:700;">'+
                            row.product_group+
                            '</span></span><br/>';
                    }
                    return '<div style="padding:10px;">'+need+description+productGroup+'</div></div>';
                }
            }, {
                targets: 5,
                data: 'crm_priority',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var crmPriority = '';
                    if (isNull(row.crm_priority)) {
                        crmPriority = '<span>Mức độ khẩn cấp: <span style="font-weight:700;">' + row.crm_priority + '</span></span><br/>';
                    }
                    var customerSource = '';
                    if (isNull(row.customer_source)) {
                        customerSource = '<span>Nguồn khách hàng: <span style="font-weight:700;">' +
                            row.customer_source +
                            '</span></span><br/>';
                    }
                    var crmType = '';
                    if (isNull(row.crm_type)) {
                        crmType = '<span>Kiểu chốt: <span style="font-weight:700;">' +
                            row.crm_type +
                            '</span></span><br/>';
                    }
                    var visit = 'Chưa đến';
                    if(row.visit==1) {
                        visit='Đã đến';
                    }
                    visit = '<span>Đến cửa hàng: <span style="font-weight:700;">' + visit + '</span></span><br/>';
                    return '<div style="padding:10px;">' + crmPriority + customerSource + crmType + visit + '</div></div>';
                }
            }, {
                targets: 6,
                data: 'user_created',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    var userCreated = '';

                    if(isNull(row.user_created_phone)) {
                        userCreated='<span>Người tạo: <span style="font-weight:700;"><a href="tel:'+
                            row.user_created_phone+
                            '">'+
                            row.user_created+
                            '</a></span></span><br/>';
                    } else {
                        userCreated='<span>Người tạo: <span style="font-weight:700;">'+
                            row.user_created+
                            '</span></span><br/>';
                    }
                    var userCares = '';
                    var condition='';
                    if(row.user_cares) {
                        if(row.user_cares.length>0) {
                            for(var i=0;i<row.user_cares.length;i++) {
                                userCares=userCares+
                                    condition+
                                    '<a href="tel:'+
                                    row.user_cares[i].phone_number+
                                    '">'+
                                    row.user_cares[i].display_name+
                                    '</a>';
                                condition=', ';
                            }
                        }
                    }
                    if(userCares) {
                        userCares='<span>Phụ trách: <span style="font-weight:700;">'+userCares+'</span></span><br/>';
                    }
                    return '<div style="padding:10px;">' + userCreated + userCares + '</div></div>';
                }
            }, {
                targets: 7,
                data: 'contact_date',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="text-align:center;">' + moment(data).format('DD-MM-YYYY hh:mm') + '</div>';
                }
            }, {
                targets: 8,
                data: 'date_created',
                className: 'datatable-cell-center',
                orderable: false,
                render: function (data, type, row, meta) {
                    return '<div style="text-align:center;">' + moment(data).format('DD-MM-YYYY hh:mm') + '</div>';
                }
            }
        ],
        order: [[8, 'desc']]
    });
}
function reloadData() {
    if (table !== null) {
        table
            .columns(0).search($('#txtCode').val())
            .columns(1).search($('#txtCustomerCode').val())
            .columns(2).search($('#txtCustomerName').val())
            .columns(3).search($('#txtCustomerPhone').val())
            .columns(4).search($('#txtCustomerAddress').val())
            .columns(5).search($('#txtProvince').val())
            .columns(6).search($('#txtDistrict').val())
            .columns(7).search($('#txtWard').val())
            .columns(8).search($('#txtCustomerGroup').val())
            .columns(9).search($('#txtCustomerVip').val())
            .columns(10).search($('#txtCustomerSource').val())
            .columns(11).search($('#txtProductGroup').val())
            .columns(12).search($('#txtCrmStatus').val())
            .columns(13).search($('#txtCrmType').val())
            .columns(14).search($('#txtCrmPriority').val())
            .columns(15).search($('#txtVisit').val())
            .columns(16).search($('#txtFromDate').val())
            .columns(17).search($('#txtToDate').val())
            .columns(18).search($('#txtUserCreated').val())
            .columns(19).search($('#txtDateCreated').val())
            .columns(20).search($('#txtUserCare').val())
            .columns(21).search($('#txtNeed').val())
            .draw();
    }
}