import React from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import NewIncident from './pages/NewIncident';
import HealthReport from './pages/HealthReport';
import Home from './pages/Home';

export default function Routes() {
    return (
        <BrowserRouter>
        <Switch>
            <Route path="/" exact component={Home} />
            <Route path="/map" exact component={HealthReport} />
            <Route path="/incidents/new" component={NewIncident} />
        </Switch>
        </BrowserRouter>
    );
}