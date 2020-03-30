import React from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import NewIncident from './pages/NewIncident';
import Consumer from './pages/Consumer';

export default function Routes() {
    return (
        <BrowserRouter>
        <Switch>
            <Route path="/" exact component={Consumer} />
            <Route path="/incidents/new" component={NewIncident} />
        </Switch>
        </BrowserRouter>
    );
}