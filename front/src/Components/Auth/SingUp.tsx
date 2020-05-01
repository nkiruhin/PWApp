import React from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import Grid from '@material-ui/core/Grid';
import Box from '@material-ui/core/Box';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import { Link } from 'react-router-dom';
import fetchApi from '../../Service/fetcher';
import Alert from '@material-ui/lab/Alert';
import { ISigInFormData } from './SingIn';

function Copyright() {
    return (
        <Typography variant="body2" color="textSecondary" align="center">
            {'Copyright © '}
            <Link color="inherit" to="https://github.com/nkiruhin/">
                Nikolay Kirukhin
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}

const useStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(3),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
}));

interface IFormData extends ISigInFormData {
    firstName: string;
    lastName: string;
    repeatPassword: string;
}

export const SignUp:React.FC = () => {
    const classes = useStyles();
    const emptyFormData: IFormData =
    {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        repeatPassword: ''
    }
    const [formData, setFormData] = React.useState<IFormData>(emptyFormData);
    const [alert, setAlert] = React.useState({ show: false, error: true, message: '' });

    const _handleSubmit = async (e: any) => {
        e.preventDefault();
        
        if (formData.password !== formData.repeatPassword) {
            setAlert({ show: true, error: true, message: "passwords don't match" })
            return;
        }
        let url = "api/Auth/Register?firstName="
            + formData.firstName
            + "&lastName="
            + formData.lastName
            + "&email="
            + formData.email
            + "&password=" + formData.password;

        let res = await fetchApi(null, url, "GET")
        if (res.status !== 200) {
            setAlert({ show: true, error: true, message: res.data.error })
        }
        else
        {
            setAlert({ show: true, error: false, message: 'Account created successfully' })
            setFormData(emptyFormData);
        }    
    }

    return (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <div className={classes.paper}>
                <Avatar className={classes.avatar}>
                    <LockOutlinedIcon />
                </Avatar>
            <Typography component="h1" variant="h5">
                    Sign up
            </Typography>
                <form onSubmit={ _handleSubmit } >
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                name="firstName"
                                variant="outlined"
                                required
                                fullWidth
                                label="First Name"
                                onChange={(evn: any) => (setFormData({ ...formData, firstName: evn.target.value }))}
                                value={formData.firstName}
                                autoFocus
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                variant="outlined"
                                fullWidth
                                label="Last Name"
                                required
                                name="lastName"
                                autoComplete="lname"
                                onChange={(evn: any) => (setFormData({ ...formData, lastName: evn.target.value }))}
                                value={formData.lastName}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                variant="outlined"
                                type = "email"
                                fullWidth
                                required
                                label="Email Address"
                                name="email"
                                autoComplete="email"
                                onChange={(evn) => (setFormData({ ...formData, email: evn.target.value }))}
                                value={ formData.email }
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                variant="outlined"
                                fullWidth
                                required
                                name="password"
                                label="Password"
                                type="password"
                                autoComplete="current-password"
                                onChange={(evn: any) => (setFormData({ ...formData, password: evn.target.value }))}
                                value={formData.password}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                variant="outlined"
                                fullWidth
                                required
                                name="repeatPassword"
                                label="Confirm Password"
                                type="password"
                                onChange={(evn: any) => (setFormData({ ...formData, repeatPassword: evn.target.value }))}
                                value={formData.repeatPassword}
                            />
                        </Grid>
                    </Grid>
                    {!!alert.show ? <Alert severity={alert.error ? "error" : "success"}>{alert.message}</Alert> : null}
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        className={classes.submit} >
                        Sign Up
                    </Button>
                    <Grid container justify="flex-end">
                        <Grid item>
                            <Link to="/" >
                                Already have an account? Sign in
                            </Link>
                        </Grid>
                    </Grid>
                </form>
            </div>
            <Box mt={5}>
                <Copyright />
            </Box>
        </Container>
    );
}
export default SignUp;