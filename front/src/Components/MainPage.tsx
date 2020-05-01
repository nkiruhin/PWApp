import React from 'react';
import Grid from '@material-ui/core/Grid';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import TransactionList from './TransactionList';
import { Container } from '@material-ui/core';
import UserCard from './UserCard';
import * as signalR from "@microsoft/signalr";
import { AppContext } from './Context/AppContext';

export const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub", { accessTokenFactory: () => localStorage.Token , transport: signalR.HttpTransportType.LongPolling })
    .build();

connection.on("ReceiveMessage", (message: string) => {
    console.log(message);
});

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            flexGrow: 1,
        },
        paper: {
            padding: theme.spacing(2),
            textAlign: 'center',
            color: theme.palette.text.secondary,
        },
        bullet: {
            display: 'inline-block',
            margin: '0 2px',
            transform: 'scale(0.8)',
        },
        title: {
            fontSize: 14,
        },
        pos: {
            marginBottom: 12,
        },
    }),
);

export const MainPage: React.FC = () => {
    
    const { state, dispatch } = React.useContext(AppContext);

    connection.on("Refresh", (isRefresh: boolean) => {
        dispatch({ type: "REFRESH", payload: { ...state, isRefresh: isRefresh } })
    });

    React.useEffect(() => {
        console.log("signalr client start");
        const startSignaR = async () => await connection.start().catch(err => console.log(err));
        startSignaR();     
    }, [])

   
    const classes = useStyles();
    return (
        <Container maxWidth="xl" style={{ paddingTop: 24 }}>
        <Grid container spacing={5}>
                <Grid item xs={3}>
                    <UserCard classes={classes} refresh={ true } />
            </Grid>
            <Grid item xs={9}>
                <TransactionList />
            </Grid>
            </Grid>
        </Container>
    )
};
 
export default MainPage;