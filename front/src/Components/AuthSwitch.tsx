import React from 'react';
import { Switch, Route } from 'react-router-dom';
import SignIn from './Auth/SingIn';
import SignUp from './Auth/SingUp';

export const AuthSwitch: React.FC = () => {

    return (
        <div className="auth">
            <Switch>
                <Route exact={true} path='/' component={SignIn} />
                <Route path='/SingUp' component={SignUp} />
            </Switch>
        </div>
    ); 
}
export default AuthSwitch;