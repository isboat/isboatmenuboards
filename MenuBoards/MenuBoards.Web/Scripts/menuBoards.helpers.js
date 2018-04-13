window.mb.helpers = {
    convertToMenuModel: function (menu) {
        var menuModel = {
            id: menu.id,
            mainMenuHeading: menu.heading,
            menuItems: []
        }

        if (menu.menuItems) {
            for (var prop in menu.menuItems) {
                if (menu.menuItems.hasOwnProperty(prop)) {
                    menuModel.menuItems.push(menu.menuItems[prop]);
                }
            }
        }

        return menuModel;
    }
}