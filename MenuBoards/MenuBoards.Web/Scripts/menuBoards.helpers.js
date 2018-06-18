window.mb.helpers = {
    convertToMenuModel: function (menu) {
        var menuModel = {
            Id: menu.Id,
            SlideId: menu.SlideId,
            MainMenuHeading: menu.Heading,
            MenuItems: []
        }

        if (menu.MenuItems) {
            for (var prop in menu.MenuItems) {
                if (menu.MenuItems.hasOwnProperty(prop)) {
                    menuModel.MenuItems.push(menu.MenuItems[prop]);
                }
            }
        }

        return menuModel;
    }
}