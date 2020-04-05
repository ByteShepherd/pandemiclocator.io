import React from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import NewReport from './pages/NewReport';
import HealthReport from './pages/HealthReport';
import Home from './pages/Home';

export default function Routes() {
    return (
        <BrowserRouter>
        <Switch>
            <Route path="/" exact component={Home} />
            <Route path="/reports" exact component={HealthReport} />
            <Route path="/reports/new" component={NewReport} />
        </Switch>
        </BrowserRouter>
    );
}