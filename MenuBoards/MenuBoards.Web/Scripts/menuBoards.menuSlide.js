window.mb.menuSlide = {
    
    Menus: [],
    
    createMenu: function (menuObj) {

        var menu = menuObj || { Id: window.newguid(), SlideId: window.mb.slideId }

        return {
            Id: menu.Id,
            Heading: menu.MainMenuHeading || "",
            SlideId: menu.SlideId,

            MenuItems: menu.MenuItems || [],
            
            templates: {
                addMenuItemModal: '#addmenuitemModal',
                addImageModal: '#addImageModal',
                deleteMenuColumnWarning: '#delMenuColWarnModal',
                $menuItemDisplay: $('#menuItemDisplayTpl'),
                settingsTpl: '#settingsTpl'
            },

            displayElements: {
                listofmenuItems: ".listofmenuitems"
            },

            getMenuCol: function() {
                return $("#" + this.Id);
            },

            createMenuItem: function (menuItemObj) {

                var item = menuItemObj || { Id: window.newguid() }

                return {
                    Id: item.Id,
                    Label: item.Label || "",
                    Price: item.Price || "",
                    SizeText: item.SizeText || "",
                    SubText: item.SubText || ""
                }
            },

            updateMenuHeading: function(val) {
                this.Heading = val;
            },

            addMenuItem: function (menuItem) {
                if (menuItem) {
                    this.MenuItems[menuItem.Id] = menuItem;
                }

                this.displayMenuItem(menuItem);

                // mark menu as unsaved
                this.Unsaved = true;
            },
            
            deleteMenuItem: function (menuItemId) {
                
                delete this.MenuItems[menuItemId];
                var ele = "#" + menuItemId;
                $(ele).remove();

                // mark menu as unsaved
                this.Unsaved = true;
            },

            displayMenuItem: function (menuItem) {
                var menuSelf = this;

                var item = $(menuSelf.templates.$menuItemDisplay.html());

                item.attr("id", menuItem.Id);
                item.find(".mi-label").html(menuItem.Label);
                item.find(".mi-size").html(menuItem.SizeText);
                item.find(".mi-price").html(menuItem.Price);
                item.find(".mi-subtext").html(menuItem.SubText);

                var displayList = this.getMenuCol().find(menuSelf.displayElements.listofmenuItems);

                var alreadyDisplayed = displayList.find("#" + menuItem.Id);
                if (alreadyDisplayed.length) {

                    // Already display, so update
                    alreadyDisplayed.html(item.html());

                } else {
                    displayList.append(item);
                }

                // Menuitem event binding
                displayList.find(".delMenuItem").on("click", function() {
                    menuSelf.deleteMenuItem($(this).parent().attr("id"));
                });

                displayList.find(".addImage").on("click", function () {
                    var menuItemId = $(this).parent().attr("id");
                    
                        menuSelf.showAddImageModal(menuItemId);
                });

                displayList.find(".editMenuItem").on("click", function () {
                    var menuItemId = $(this).parent().attr("id");
                    var toDisplay = menuSelf.MenuItems[menuItemId];

                    if (toDisplay) {
                        menuSelf.showMenuItemModal(toDisplay);
                    }
                });
            },

            showAddImageModal: function (menuItemId) {

                var self = this;

                $(self.templates.addImageModal).modal({
                    show: true,
                    keyboard: true,
                    focus: true
                });
                
                $(self.templates.addImageModal).on('shown.bs.modal',
                    function (e) {

                        window.mb.httpWrapper.get({
                            url: "/ImagePicker/LoadImages",
                            success: function(htmlData) {

                                if (htmlData) {

                                    $(self.templates.addImageModal).find(".modal-body").html(htmlData);

                                    //    $(self.templates.addImageModal).find(".addmenuitem").on('click',
                                    //    function() {
                                            
                                    //        menuItem.SubText =
                                    //            $(self.templates.addMenuItemModal).find(".menuitem-subtext").val();

                                    //        self.addMenuItem(menuItem);

                                    //        $(self.templates.addMenuItemModal).modal("hide");
                                    //    });

                                    //$(self.templates.addMenuItemModal).on('hide.bs.modal',
                                    //    function(e) {
                                    //        $(self.templates.addMenuItemModal).off();
                                    //        $(self.templates.addMenuItemModal).find(".addmenuitem").off();
                                    //    });
                                }
                            },
                            error: function (error) {

                                console.log(error);
                                $(self.templates.addImageModal).find(".modal-body").html(error.responseText);
                            }
                        });
                    });
            },

            showMenuItemModal: function (menuItem) {

                var self = this;

                $(self.templates.addMenuItemModal).modal({
                    show: true,
                    keyboard: true,
                    focus: true
                });

                $(self.templates.addMenuItemModal).find(".menuitem-id").val(menuItem.Id);
                $(self.templates.addMenuItemModal).find(".menuitem-label").val(menuItem.Label);
                $(self.templates.addMenuItemModal).find(".menuitem-size").val(menuItem.SizeText);
                $(self.templates.addMenuItemModal).find(".menuitem-price").val(menuItem.Price);
                $(self.templates.addMenuItemModal).find(".menuitem-subtext").val(menuItem.SubText);
                
                $(self.templates.addMenuItemModal).on('shown.bs.modal',
                    function(e) {
                        $(self.templates.addMenuItemModal).find(".addmenuitem").on('click', function () {

                            menuItem.Id = $(self.templates.addMenuItemModal).find(".menuitem-id").val();
                            menuItem.Label = $(self.templates.addMenuItemModal).find(".menuitem-label").val();
                            menuItem.SizeText = $(self.templates.addMenuItemModal).find(".menuitem-size").val();
                            menuItem.Price = $(self.templates.addMenuItemModal).find(".menuitem-price").val();
                            menuItem.SubText = $(self.templates.addMenuItemModal).find(".menuitem-subtext").val();

                            self.addMenuItem(menuItem);

                            $(self.templates.addMenuItemModal).modal("hide");
                        });
                    });

                $(self.templates.addMenuItemModal).on('hide.bs.modal',
                    function (e) {
                        $(self.templates.addMenuItemModal).off();
                        $(self.templates.addMenuItemModal).find(".addmenuitem").off();
                    });
            },

            bindBtnEvts: function () {
                var self = this;
                var eleid = "#" + self.Id;

                $(eleid + " .toaddmenuitembtn").on("click", function () {

                    var newMenuItem = self.createMenuItem();
                    self.showMenuItemModal(newMenuItem);
                });

                // Handling menu main heading
                $(eleid + " .menu-heading").on("blur", function () {
                    self.updateMenuHeading($(this).val());
                });

                // Handling save menu
                $(eleid + " .saveMenuBtn").on("click", function () {
                    self.saveMenu();
                });

                // Handling delete menu
                $(eleid + " .deleteMenuColX").on("click", function () {
                    self.showDeleteMenuColumnWarning();
                });
            },

            unBindBtnEvts: function () {
                var self = this;
                var eleid = "#" + self.Id;

                $(eleid + " .toaddmenuitembtn").off();

                $(eleid + " .saveMenuBtn").off();

                $(eleid + " .deleteMenuColX").off();
            },

            showDeleteMenuColumnWarning: function () {
                var self = this;

                $(self.templates.deleteMenuColumnWarning).modal({
                    show: true,
                    keyboard: true,
                    focus: true
                });

                $(self.templates.deleteMenuColumnWarning).on('shown.bs.modal',
                    function (e) {
                        $(self.templates.deleteMenuColumnWarning).find(".deletemenuCol").on('click', function () {
                            
                            window.mb.menuSlide.deleteMenuCol(self.Id);

                            $(self.templates.deleteMenuColumnWarning).modal("hide");
                        });
                    });

                $(self.templates.deleteMenuColumnWarning).on('hide.bs.modal',
                    function (e) {
                        $(self.templates.deleteMenuColumnWarning).off();
                        $(self.templates.deleteMenuColumnWarning).find(".deletemenuCol").off();
                    });
            },

            saveMenu: function () {

                var menuModel = window.mb.helpers.convertToMenuModel(this);

                window.mb.httpWrapper.post({
                    data: menuModel,
                    url: '/Slide/SaveMenu',
                    success: function (response) {
                    },
                    error: function () {}
                });
            },

            deleteEntireMenuCol: function() {
                
            },

            renderMenuCol: function () {

                var menuColTpl = $("#menuColTpl").html();
                var $menuColTpl = $(menuColTpl);
                $menuColTpl.attr("id", this.Id);
                $menuColTpl.find(".menu-heading").val(this.Heading);

                $menuColTpl.find('.panel-body').append(this.renderMenuForm());
                $(".menusSection .row").append($menuColTpl);

                if (this.MenuItems) {
                    for (var i = 0; i < this.MenuItems.length; i++) {
                        this.displayMenuItem(this.MenuItems[i]);
                    }
                }

                // Bind DOM events
                this.bindBtnEvts();

                return $menuColTpl;
            },

            renderMenuForm: function () {
                var menuTpl = $("#menuTpl").html();

                var $menuTpl = $(menuTpl);
                $menuTpl.find(".menu-heading").val(this.Heading);

                return $menuTpl;
            },
        }
    },

    addNewMenuCol: function () {
        var menu = this.createMenu();
        menu.isNew = true;

        this.Menus.push(menu);
        
        $(".menusSection .row").append(menu.renderMenuCol());
    },

    LoadSlideMenus: function () {
        
        var self = this;
        window.mb.httpWrapper.get({
            url: '/Slide/SlideMenus?slideId=' + window.mb.slideId,
            success: function (response) {

                if (response) {

                    for (var i = 0; i < response.length; i++) {

                        var menu = self.createMenu(response[i]);
                        self.Menus.push(menu);

                        $(".menusSection .row").append(menu.renderMenuCol());
                    }
                }

            },
            error: function () { }
        });
    },

    deleteMenuCol: function(menuId) {
        var self = this;

        var menuToDelete;
        var index = -1;

        var delFunc = function() {
            self.Menus.splice(index, 1);

            var ele = "div#" + menuToDelete.Id;
            $(ele).remove();
        };

        for (var i = 0; i < this.Menus.length; i++) {
            if (this.Menus[i].Id === menuId) {
                menuToDelete = this.Menus[i];
                index = i;
                break;
            }
        }
        
        if (menuToDelete) {

            if (menuToDelete.isNew) {

                delFunc();
            } else {

                window.mb.httpWrapper.delete({
                    data: menuToDelete.Id,
                    url: '/Slide/DeleteMenu',
                    success: function (response) {
                        delFunc();
                    },
                    error: function () { }
                });
            }
        }
    }

    /*Design Settings*/
}

$("#addMenuCol").on("click", function () {
    window.mb.menuSlide.addNewMenuCol();
});

$("#showsettingsbtn").on("click", function () {
    window.mb.designSettings.showSettings(window.mb.slideId);
});

$("#showdisplaysettingsbtn").on("click", function () {
    window.mb.displaySettings.showSettings(window.mb.slideId);
});

// Initial menu load
window.mb.menuSlide.LoadSlideMenus();