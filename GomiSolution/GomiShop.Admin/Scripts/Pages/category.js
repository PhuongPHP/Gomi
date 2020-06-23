var megaCate = {
    cache: {
        $modal: $("#modalCate"),
        $cont: $("#mega-list"),
        $pagination: $("#mega-pagination"),
        $select: $("#selectMegaCate"),
        pageNo: 1,
        totalPage: 0,
        totalRows: 0,
        nameAjax: "getMegaCate",
        itemActive: "" // Danh mục được chọn
    },

    getHtmlFrm: new function () { },// Get Html From(Add, Update) Mega Category
    submitFrm: new function () { },// Submit From(Add, Update) Mega Category
    updateStatus: new function () { },// Update Status Mega Category
    getList: function () { },// Get List Mega Category
}

var cate = {
    cache: {
        $modal: $("#modalCate"),
        $cont: $("#cate-list"),
        $pagination: $("#cate-pagination"),
        $select: $("#selectCate"),
        pageNo: 1,
        totalPage: 0,
        totalRows: 0,
        nameAjax: "getCate",
        itemActive: "", // Danh mục được chọn
        parentId: ""
    },

    getHtmlFrm: new function () { },// Get Html From(Add, Update) Mega Category
    submitFrm: new function () { },// Submit From(Add, Update) Mega Category
    updateStatus: new function () { },// Update Status Mega Category
    getList: function () { },// Get List Mega Category
}


var subCate = {
    cache: {
        $modal: $("#modalCate"),
        $cont: $("#subCate-list"),
        $pagination: $("#subCate-pagination"),
        $select: $("#selectSubCate"),
        pageNo: 1,
        totalPage: 0,
        totalRows: 0,
        nameAjax: "getSubCate",
        itemActive: "", // Danh mục được chọn
        parentId: ""
    },

    getHtmlFrm: new function () { },// Get Html From(Add, Update) Mega Category
    submitFrm: new function () { },// Submit From(Add, Update) Mega Category
    updateStatus: new function () { },// Update Status Mega Category
    getList: function () { },// Get List Mega Category
}


$(function () {
    /*======================*
     *     Mega Category    *
     *======================*/
    megaCate.getHtmlFrm = new ajax_getHtmlFormMegaCate();
    megaCate.submitFrm = new ajax_submitFormMegaCate();
    megaCate.updateStatus = new ajax_updateStatusMegaCate();
    megaCate.getList = new ajax_getList(megaCate);
    megaCate.getList.init();
    megaCate.getList.getData();

    select_megaCate.init();

    /*======================*
     *      Category        *
     *======================*/
    cate.getHtmlFrm = new ajax_getHtmlFormCate();
    cate.submitFrm = new ajax_submitFormCate();
    cate.updateStatus = new ajax_updateStatusCate();
    cate.getList = new ajax_getList(cate);
    cate.getList.init();
    select_cate.init();


    /*======================*
     *      Sub Category    *
     *======================*/
    subCate.getHtmlFrm = new ajax_getHtmlFormSubCate();
    subCate.submitFrm = new ajax_submitFormSubCate();
    subCate.updateStatus = new ajax_updateStatusSubCate();
    subCate.getList = new ajax_getList(subCate);
    subCate.getList.init();
    select_subCate.init();

});


/*=================================
*          Mega Category
===================================*/
var BaseAjaxFormMegaCate = function ($modal) {
    this.OnBegin = function () {
        extend.loading("body", true, true);
    }

    this.OnSuccess = function () {

    }

    this.OnComplete = function () {
        //  $modal.modal("hide");
        if ($modal.find("form").length > 0) {
            $.validator.unobtrusive.parse($modal.find("form"));

            // upload Avatar
            $modal.find("form").find("#uploadAvatar").uploadImgSingle({
                urlAjaxUpload: "admin/upload/file-temp",
                inputTarget: "input#Avatar",
                rotate: false
            });

            // upload Cover
            $modal.find("form").find("#uploadCover").uploadImgSingle({
                urlAjaxUpload: "admin/upload/file-temp",
                inputTarget: "input#Cover",
                rotate: false
            });
        }
    }
}

var ajax_getHtmlFormMegaCate = function () {
    this.super_ = BaseAjaxFormMegaCate;
    this.super_.call(this, megaCate.cache.$modal);

    this.OnSuccess = function (data) {
        extend.loading("body", false);

        if (data.status) {
            megaCate.cache.$modal.modal('show');
            megaCate.cache.$modal.find(".modal-content").html("");
            megaCate.cache.$modal.find(".modal-content").html(data.view);
        } else {
            alert(data.message);
        }
    }

}

var ajax_submitFormMegaCate = function () {
    // gán this.super_ = baseAjaxAddCate
    // để hiểu rằng lớp cha của submitFrmAddCate là baseAjaxAddCate
    this.super_ = BaseAjaxFormMegaCate;
    this.super_.call(this, megaCate.cache.$modal);

    this.OnBegin = function () {
        extend.loading(megaCate.cache.$modal.find('.modal-content'), true);
        extend.modal(megaCate.cache.$modal, false);
    }

    this.OnSuccess = function (data) {
        megaCate.cache.$modal.find(".modal-content").html("");
        megaCate.cache.$modal.find(".modal-content").html(data.view);

        if (data.status) { // Success
            toastr["success"](data.message);
            // refresh List
            megaCate.getList.getData(select_megaCate.selectItem);
        } else {
            if (data.error != null) { // Error Model Valid
                megaCate.cache.$modal.modal('hide');
                alert(data.message);// Error Code
            }
        }

        extend.loading(megaCate.cache.$modal.find('.modal-content'), false);
        extend.modal(megaCate.cache.$modal, true);
    }
}

var ajax_updateStatusMegaCate = function () {
    this.OnBegin = function () {
        extend.loading("body", true, true);
    }

    this.OnSuccess = function (dataResp) {
        if (dataResp.status) {
            toastr["success"](dataResp.message);
            // refresh List
            megaCate.getList.getData(select_megaCate.selectItem);
        } else {
            toastr["warning"](dataResp.message);
        }
        extend.loading("body", false);
    }
}

// Xử lý khi chọn danh mục
var select_megaCate = (function () {

    var init = function () {

        bindEvent();
    }

    var bindEvent = function () {
        // init plugin Select2
        selectCategory("#" + megaCate.cache.$select.attr("id"), "mega/select-search", formatSelectMegaCate)

        // Select 2
        $(document).on('change', "#" + megaCate.cache.$select.attr("id"), function () {
            megaCate.cache.itemActive = megaCate.cache.$select.val();
            megaCate.getList.refreshDatas(selectItem);
        });

        // Select item in List
        $(document).on("click", "#mega-list .cate-item", function () {
            megaCate.cache.itemActive = $(this).attr("data-id");

            selectItem();
        });

    }

    // Chọn danh mục
    var selectItem = function () {
        $("#mega-list .cate-item").removeClass("active");
        if (megaCate.cache.itemActive) { // Nếu có giá trị được chọn
            $("#mega-list .cate-item[data-id=" + megaCate.cache.itemActive + "]").addClass("active");
        }
        mappingToCate();
    }



    // Thay đổi id Mega với Cate khi Click chọn
    var mappingToCate = function () {

        $("form#frmGetHtmlCate").find("input#MegaId").val(megaCate.cache.itemActive);
        if (megaCate.cache.itemActive) {
            $("form#frmGetHtmlCate").find("button[type='submit']").prop("disabled", false);
        } else {
            $("form#frmGetHtmlCate").find("button[type='submit']").prop("disabled", true);
        }

        // Tải lại danh sách children
        refreshCate();
    }

    return {
        init: init,
        selectItem: selectItem
    }
})();

// format hiện thị trong select list
function formatSelectMegaCate(repo) {
    if (repo.loading) {
        return repo.text;
    }

    var markup = "<div class='select-item-search'>" +
        "<img class='icon' src='" + repo.Avatar + "' />" +
        "<div class='select-item-title'>" + repo.Name_vi + "</div>" +
        "</div>";
    return markup;
}


/*================================*
 *           Category             *
 *================================*/
var BaseAjaxFormCate = function ($modal) {
    this.OnBegin = function () {
        extend.loading("body", true, true);
    }

    this.OnSuccess = function () {

    }

    this.OnComplete = function () {
        $modal.modal("hide");

        if ($modal.find("form").length > 0) {
            $.validator.unobtrusive.parse($modal.find("form"));
        }
    }
}

var ajax_getHtmlFormCate = function () {
    this.super_ = BaseAjaxFormCate;
    this.super_.call(this, cate.cache.$modal);

    this.OnSuccess = function (data) {
        extend.loading("body", false);

        if (data.status) {
            cate.cache.$modal.modal('show');
            cate.cache.$modal.find(".modal-content").html("");
            cate.cache.$modal.find(".modal-content").html(data.view);
        } else {
            alert(data.message);
        }
    }

}

var ajax_submitFormCate = function () {
    // gán this.super_ = baseAjaxAddCate
    // để hiểu rằng lớp cha của submitFrmAddCate là baseAjaxAddCate
    this.super_ = BaseAjaxFormCate;
    this.super_.call(this, cate.cache.$modal);

    this.OnBegin = function () {
        extend.loading(cate.cache.$modal.find('.modal-content'), true);
        extend.modal(cate.cache.$modal, false);
    }

    this.OnSuccess = function (data) {
        cate.cache.$modal.find(".modal-content").html("");
        cate.cache.$modal.find(".modal-content").html(data.view);

        if (data.status) { // Success
            toastr["success"](data.message);
            // refresh List
            cate.getList.getData(select_cate.selectItem);
        } else {
            if (data.error != null) { // Error Model Valid
                cate.cache.$modal.modal('hide');
                alert(data.message);// Error Code
            }
        }

        extend.loading(cate.cache.$modal.find('.modal-content'), false);
        extend.modal(cate.cache.$modal, true);
    }
}

var ajax_updateStatusCate = function () {
    this.OnBegin = function () {
        extend.loading("body", true, true);
    }

    this.OnSuccess = function (dataResp) {
        if (dataResp.status) {
            toastr["success"](dataResp.message);
            // refresh List
            cate.getList.getData(select_cate.selectItem);
        } else {
            toastr["warning"](dataResp.message);
        }
        extend.loading("body", false);
    }
}

// Xử lý khi chọn danh mục
var select_cate = (function () {

    var init = function () {
        bindEvent();
    }

    var bindEvent = function () {
        // init plugin Select2
        selectCategory("#" + cate.cache.$select.attr("id"), "cate/select-search", formatSelectCate, cate.cache.parentId)

        // Select 2
        $(document).on('change', "#" + cate.cache.$select.attr("id"), function () {
            cate.cache.itemActive = cate.cache.$select.val();
            cate.getList.refreshDatas(selectItem);
        });

        // Select item in List
        $(document).on("click", "#cate-list .cate-item", function () {
            cate.cache.itemActive = $(this).attr("data-id");
            selectItem();
        });

    }

    // Chọn danh mục
    var selectItem = function () {
        $("#cate-list .cate-item").removeClass("active");
        if (cate.cache.itemActive) { // Nếu có giá trị được chọn
            $("#cate-list .cate-item[data-id=" + cate.cache.itemActive + "]").addClass("active");
        }
        mappingToSubCate();
    }



    // Thay đổi parentId của SubCate khi Click chọn
    var mappingToSubCate = function () {

        $("form#frmGetHtmlSubCate").find("input#CateId").val(cate.cache.itemActive);
        if (cate.cache.itemActive) {
            $("form#frmGetHtmlSubCate").find("button[type='submit']").prop("disabled", false);
        } else {
            $("form#frmGetHtmlSubCate").find("button[type='submit']").prop("disabled", true);
        }

        // Tải lại danh sách children
        refreshSubCate();
    }

    return {
        init: init,
        selectItem: selectItem
    }
})();

// format hiện thị trong select list
function formatSelectCate(repo) {
    if (repo.loading) {
        return repo.text;
    }

    var markup = "<div class='select-item-search'>" +
        "<div class='select-item-title'>" + repo.Name_vi + "</div>" +
        "</div>";
    return markup;
}

var refreshCate = function () {
    cate.cache.parentId = megaCate.cache.itemActive || 0;
    cate.cache.$select.val(null).trigger("change");;
    cate.getList.refreshDatas();

    refreshSubCate();

}


/*================================*
 *           Sub Category             *
 *================================*/
var BaseAjaxFormSubCate = function ($modal) {
    this.OnBegin = function () {
        extend.loading("body", true, true);
    }

    this.OnSuccess = function () {

    }

    this.OnComplete = function () {
        $modal.modal("hide");

        if ($modal.find("form").length > 0) {
            $.validator.unobtrusive.parse($modal.find("form"));
        }
    }
}

var ajax_getHtmlFormSubCate = function () {
    this.super_ = BaseAjaxFormSubCate;
    this.super_.call(this, subCate.cache.$modal);

    this.OnSuccess = function (data) {
        extend.loading("body", false);

        if (data.status) {
            subCate.cache.$modal.modal('show');
            subCate.cache.$modal.find(".modal-content").html("");
            subCate.cache.$modal.find(".modal-content").html(data.view);
        } else {
            alert(data.message);
        }
    }

}

var ajax_submitFormSubCate = function () {
    // gán this.super_ = baseAjaxAddCate
    // để hiểu rằng lớp cha của submitFrmAddCate là baseAjaxAddCate
    this.super_ = BaseAjaxFormSubCate;
    this.super_.call(this, subCate.cache.$modal);

    this.OnBegin = function () {
        extend.loading(subCate.cache.$modal.find('.modal-content'), true);
        extend.modal(subCate.cache.$modal, false);
    }

    this.OnSuccess = function (data) {
        subCate.cache.$modal.find(".modal-content").html("");
        subCate.cache.$modal.find(".modal-content").html(data.view);

        if (data.status) { // Success
            toastr["success"](data.message);
            // refresh List
            subCate.getList.getData(select_subCate.selectItem);
        } else {
            if (data.error != null) { // Error Model Valid
                subCate.cache.$modal.modal('hide');
                alert(data.message);// Error Code
            }
        }

        extend.loading(subCate.cache.$modal.find('.modal-content'), false);
        extend.modal(subCate.cache.$modal, true);
    }
}

var ajax_updateStatusSubCate = function () {
    this.OnBegin = function () {
        extend.loading("body", true, true);
    }

    this.OnSuccess = function (dataResp) {
        if (dataResp.status) {
            toastr["success"](dataResp.message);
            // refresh List
            subCate.getList.getData(select_subCate.selectItem);
        } else {
            toastr["warning"](dataResp.message);
        }
        extend.loading("body", false);
    }
}

// Xử lý khi chọn danh mục
var select_subCate = (function () {

    var init = function () {
        bindEvent();
    }

    var bindEvent = function () {
        // init plugin Select2
        selectCategory("#" + subCate.cache.$select.attr("id"), "sub-cate/select-search", formatSelectSubCate, subCate.cache.parentId)

        // Select 2
        $(document).on('change', "#" + subCate.cache.$select.attr("id"), function () {
            subCate.cache.itemActive = subCate.cache.$select.val();
            subCate.getList.refreshDatas(selectItem);
        });

        // Select item in List
        $(document).on("click", "#subCate-list .cate-item", function () {
            subCate.cache.itemActive = $(this).attr("data-id");
            selectItem();
        });

    }

    // Chọn danh mục
    var selectItem = function () {
        $("#subCate-list .cate-item").removeClass("active");
        if (subCate.cache.itemActive) { // Nếu có giá trị được chọn
            $("#subCate-list .cate-item[data-id=" + subCate.cache.itemActive + "]").addClass("active");
        }
    }


    return {
        init: init,
        selectItem: selectItem
    }
})();

// format hiện thị trong select list
function formatSelectSubCate(repo) {
    if (repo.loading) {
        return repo.text;
    }

    var markup = "<div class='select-item-search'>" +
        "<div class='select-item-title'>" + repo.Name_vi + "</div>" +
        "</div>";
    return markup;
}


var refreshSubCate = function () {
    subCate.cache.parentId = cate.cache.itemActive || 0;
    subCate.cache.$select.val(null).trigger("change");;
    subCate.getList.refreshDatas();
}

/*============ COMMON ==================
* cache{ nameAjax, $cont, totalPage, totalRows, pageNo, idSelect, $pagination }
 * 
 */
var ajax_getList = function (model) {

    this.init = function () {
        this.bindEvent();
    }

    this.bindEvent = function () {
        model.cache.$pagination.on('click', ".btn", this.pagination.bind(this, "click"));
    }

    this.getData = function (callback) {
        extend.loading(".cate-box", true);
        var data = this.getParams();
        var $this = this;
        APP.Data.Category[model.cache.nameAjax](data, function (dataResp) {
            if (dataResp.status) {
                model.cache.$cont.html(dataResp.view);
                model.cache.totalPage = dataResp.totalPage;
                model.cache.totalRows = dataResp.totalRows;
                $this.checkPagination();
                if (callback)
                    callback();
            } else {
                if (dataResp.error != null)
                    toastr["warning"](dataResp.message);
            }
            extend.loading(".cate-box", false);
        });
    }

    this.getParams = function () {
        var data = {};
        data.pageNo = model.cache.pageNo;
        data.id = $(model.cache.$select).val() || 0;
        // thêm Id danh mục cha đã chọn
        if (model.cache.parentId != null && model.cache.parentId != undefined) {
            data.parentId = model.cache.parentId || 0;
        }
        return data;
    }

    this.pagination = function (e, target) {
        var $btn = $(target.currentTarget);
        if ($btn.attr("data-page") === "Previous") {
            model.cache.pageNo--;
        } else {
            model.cache.pageNo++;
        }

        this.getData();
    }

    this.checkPagination = function () {
        if (model.cache.totalPage <= 1) {
            model.cache.$pagination.find(".btn[data-page='Next']").prop("disabled", true);
            model.cache.$pagination.find(".btn[data-page='Previous']").prop("disabled", true);
        } else {

            if (model.cache.pageNo === 1) {
                model.cache.$pagination.find(".btn[data-page='Next']").prop("disabled", false);
                model.cache.$pagination.find(".btn[data-page='Previous']").prop("disabled", true);
            }

            if (model.cache.pageNo > 1 && model.cache.pageNo < model.cache.totalPage) {
                model.cache.$pagination.find(".btn[data-page='Next']").prop("disabled", false);
                model.cache.$pagination.find(".btn[data-page='Previous']").prop("disabled", false);
            }

            if (model.cache.pageNo >= model.cache.totalPage) {
                model.cache.$pagination.find(".btn[data-page='Next']").prop("disabled", true);
                model.cache.$pagination.find(".btn[data-page='Previous']").prop("disabled", false);
            }
        }
    }

    this.refreshDatas = function (callback) {
        model.cache.pageNo = 1;
        this.getData(callback);
    }

    this.clearHtml = function () {
        model.cache.$cont.html("");
    }
}

function selectCategory(element, url, formatResp, parentId) {
    var options = {
        language: 'vi',
        ajax: {
            width: 'resolve',
            url: location.origin + "/admin/category/" + url,
            type: "POST",
            dataType: 'json',
            delay: 250,
            data: function (params) {
                var data = {};
                var form = $('#__AjaxAntiForgeryForm');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                data.__RequestVerificationToken = token;
                data.keyword = params.term;
                data.pageNo = params.page || 1;
                if (parentId != null && parentId != undefined) {
                    data.parentId = parentId || 0;
                }
                return data;
            },
            processResults: function (data, params) {
                params.page = params.page || 1;

                var select2Data = $.map(data.items, function (obj) {
                    obj.id = obj.Id;
                    obj.text = obj.Name_vi;

                    return obj;
                });

                return {
                    results: select2Data,
                    pagination: {
                        more: (params.page * data.itemPerPage) < data.totalRows
                    }
                };
            },
        },
        allowClear: true,
        placeholder: 'Tìm kiếm danh mục',
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 3,
        templateResult: formatResp,
        templateSelection: formatRepoSelection
    }
    $(element).select2(options);
}

function formatRepoSelection(repo) {
    return repo.Name_vi || repo.text;
}
