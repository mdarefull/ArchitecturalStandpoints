"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var dashboard_component_1 = require("./components/dashboard.component");
var dashboardRoutes = [
    {
        path: '',
        component: dashboard_component_1.DashboardComponent
    }
];
exports.DashboardRouting = router_1.RouterModule.forChild(dashboardRoutes);
//# sourceMappingURL=dashboard.routing.js.map