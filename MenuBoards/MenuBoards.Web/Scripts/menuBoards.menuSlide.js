window.menuBoards.menuSlide = {

    menus: [],
    
    createMenu: function (menuObj) {

        var menu = menuObj || { id: window.newguid() }

        return {
            id: menu.id,
            heading: menu.heading || "",

            menuItems: menu.menuItems || [],
            
            templates: {
                addMenuItemModal: '#addmenuitemModal',
                deleteMenuColumnWarning: '#delMenuColWarnModal',
                $menuItemDisplay: $('#menuItemDisplayTpl'),
                settingsTpl: '#settingsTpl'
            },

            displayElements: {
                listofmenuItems: ".listofmenuitems"
            },

            getMenuCol: function() {
                return $("#" + this.id);
            },

            createMenuItem: function (menuItemObj) {

                var item = menuItemObj || { id: window.newguid() }

                return {
                    id: item.id,
                    label: item.label || "",
                    price: item.price || "",
                    sizetext: item.sizetext || "",
                    subtext: item.subtext || ""
                }
            },

            updateMenuHeading: function(val) {
                this.heading = val;
            },

            addMenuItem: function (menuItem) {
                if (menuItem) {
                    this.menuItems[menuItem.id] = menuItem;
                }

                this.displayMenuItem(menuItem);

                // mark menu as unsaved
                this.unsaved = true;
            },

            displayMenuItem: function (menuItem) {
                var menuSelf = this;

                var item = $(menuSelf.templates.$menuItemDisplay.html());

                item.find(".mi-label").html(menuItem.label);
                item.find(".mi-size").html(menuItem.sizetext);
                item.find(".mi-price").html(menuItem.price);
                item.find(".mi-subtext").html(menuItem.subtext);

                this.getMenuCol().find(menuSelf.displayElements.listofmenuItems).append(item);

            },

            showMenuItemModal: function (menuItem) {

                var self = this;

                $(self.templates.addMenuItemModal).modal({
                    show: true,
                    keyboard: true,
                    focus: true
                });

                $(self.templates.addMenuItemModal).find(".menuitem-label").val(menuItem.label);
                $(self.templates.addMenuItemModal).find(".menuitem-size").val(menuItem.sizetext);
                $(self.templates.addMenuItemModal).find(".menuitem-price").val(menuItem.price);
                $(self.templates.addMenuItemModal).find(".menuitem-subtext").val(menuItem.subtext);
                
                $(self.templates.addMenuItemModal).on('shown.bs.modal',
                    function(e) {
                        $(self.templates.addMenuItemModal).find(".addmenuitem").on('click', function () {

                            menuItem.label = $(self.templates.addMenuItemModal).find(".menuitem-label").val();
                            menuItem.sizetext = $(self.templates.addMenuItemModal).find(".menuitem-size").val();
                            menuItem.price = $(self.templates.addMenuItemModal).find(".menuitem-price").val();
                            menuItem.subtext = $(self.templates.addMenuItemModal).find(".menuitem-subtext").val();

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
                var eleid = "#" + self.id;

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
                var eleid = "#" + self.id;

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
                            
                            window.menuBoards.menuSlide.deleteMenuCol(self.id);

                            $(self.templates.deleteMenuColumnWarning).modal("hide");
                        });
                    });

                $(self.templates.deleteMenuColumnWarning).on('hide.bs.modal',
                    function (e) {
                        $(self.templates.deleteMenuColumnWarning).off();
                        $(self.templates.deleteMenuColumnWarning).find(".deletemenuCol").off();
                    });
            },

            renderMenuForm: function () {
                var menuTpl = $("#menuTpl").html();

                var $menuTpl = $(menuTpl);
                $menuTpl.find(".menu-heading").val(this.heading);

                return $menuTpl;
            },

            saveMenu: function () {

                var menuModel = window.menuBoards.helpers.convertToMenuModel(this);
                console.log(menuModel);

                window.menuBoards.httpWrapper.post({
                    data: menuModel,
                    url: '/Slide/SaveMenu',
                    success: function (response) {
                        console.log(response);
                    },
                    error: function () {}
                });
            },

            deleteEntireMenuCol: function() {
                
            },

            renderMenuCol: function () {

                var menuColTpl = $("#menuColTpl").html();
                var $menuColTpl = $(menuColTpl);
                $menuColTpl.attr("id", this.id);

                $menuColTpl.find('.panel-body').append(this.renderMenuForm());
                $(".menusSection .row").append($menuColTpl);


                // Bind DOM events
                this.bindBtnEvts();

                return $menuColTpl;
            }
        }
    },

    addMenuCol: function () {
        var menu = this.createMenu();
        menu.isNew = true;

        this.menus.push(menu);
        
        $(".menusSection .row").append(menu.renderMenuCol());
    },

    deleteMenuCol: function(menuId) {
        var self = this;

        var menuToDelete;
        var index = -1;

        var delFunc = function() {
            self.menus.splice(index, 1);

            var ele = "div#" + menuToDelete.id;
            $(ele).remove();
        };

        for (var i = 0; i < this.menus.length; i++) {
            if (this.menus[i].id === menuId) {
                menuToDelete = this.menus[i];
                index = i;
                break;
            }
        }
        
        if (menuToDelete) {

            if (menuToDelete.isNew) {

                delFunc();
            } else {

                window.menuBoards.httpWrapper.delete({
                    data: menuToDelete.id,
                    url: '/Slide/DeleteMenu',
                    success: function (response) {
                        delFunc();
                    },
                    error: function () { }
                });
            }
        }
    }
}

$("#addMenuCol").on("click", function () {
    window.menuBoards.menuSlide.addMenuCol();
});

$("#showsettingsbtn").on("click", function () {
    window.menuBoards.designSettings.showSettings();
});