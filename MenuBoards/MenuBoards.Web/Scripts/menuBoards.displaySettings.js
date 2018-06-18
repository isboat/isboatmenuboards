window.mb.displaySettings = {

    Id: null,
    GoLiveDatetime: null,
    SlideId: null,

    templates: {
        mainSettings: "#displaySettingsTpl",
    },

    loadDesignSettings: function (slideId, callbackFun) {
        var self = this;

        window.mb.httpWrapper.get({
            url: '/Slide/GetDisplaySettings/?slideId=' + slideId ,
            success: function (response) {

                if (response !== "error") {

                    self.Id = response.Id;
                    self.GoLiveDatetime = response.GoLiveDatetime;
                    self.SlideId = response.SlideId;
                    self.Disable = response.Disable;
                    
                    if (callbackFun) {
                        callbackFun();
                    }
                }
            },
            error: function () { }
        });
    },

    showSettings: function (slideId) {
        var self = this;
        self.loadDesignSettings(slideId, function () {

            $(self.templates.mainSettings).modal({
                show: true,
                keyboard: true,
                focus: true
            });
            
            $(self.templates.mainSettings).on('shown.bs.modal',
            function (e) {

                $("#ds-goLive").datetimepicker();

                $(self.templates.mainSettings).find(".saveDesignSettings").on('click', function () {
                    self.saveSettings();
                });
            });

            $(self.templates.mainSettings).on('hide.bs.modal',
                function (e) {
                    $(self.templates.mainSettings).off();
                    $(self.templates.mainSettings).find(".saveDesignSettings").off();
                });
        });
        
    },

    saveSettings: function() {
        var self = this;
        
        self.GoLiveDatetime = $(self.templates.mainSettings).find(".ds-goLive").val();
        self.Disable = $(self.templates.mainSettings).find(".ds-goLive").val();

        var url = '/Slide/SaveDisplaySettings';

        window.mb.httpWrapper.post({
            url: url,
            data: {
                Id: self.Id || window.newguid(),
                SlideId: window.mb.slideId,
                GoLiveDatetime: self.GoLiveDatetime,
                Disable: false //self.Disable
            },
            success: function(response) {
                if (response && response.Success) {
                    $(self.templates.mainSettings).modal("hide");
                }
            },
            error: function() {}
        });
    }
}