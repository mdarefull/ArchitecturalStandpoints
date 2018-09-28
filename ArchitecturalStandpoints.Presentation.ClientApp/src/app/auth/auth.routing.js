"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var login_component_1 = require("./components/login.component");
var authRoutes = [
    {
        path: '',
        component: login_component_1.LoginComponent
    }
];
exports.AuthRouting = router_1.RouterModule.forChild(authRoutes);
//# sourceMappingURL=auth.routing.js.map