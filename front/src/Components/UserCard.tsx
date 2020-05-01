import React from 'react';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import CardActions from '@material-ui/core/CardActions';
import Button from '@material-ui/core/Button';
import { TransactionDialog, ITransactionProps } from './TransactionDialog';
import fetchApi from '../Service/fetcher';
import CircularProgress from '@material-ui/core/CircularProgress';
import { AppContext } from './Context/AppContext';

class Balance {
    userName: string = '';
    userEmail: string = '';
    userId: string = '';
    currentBalance: number = 0;
    dateUpdate: Date = new Date();
    dateCreate: Date = new Date();
}


export const UserCard: React.FC<any> = (props) => {

    const { state, dispatch } = React.useContext(AppContext);

    const [showDialog, setShowDialog] = React.useState(false);
    const [balance, setBalance] = React.useState<Balance>(new Balance());
    const [showSpinner, setShowSpinner] = React.useState(false);

    const _updateContext = React.useCallback((isRefresh: boolean, balance: number) =>
        dispatch({ type: "REFRESH", payload: { balance: balance, isRefresh: isRefresh } })
        , [dispatch]); 

    React.useEffect(() => {
        setShowSpinner(true)
        const _getBalance = async () => {
            const url = "api/Balance/"
            const { data, status } = await fetchApi(null, url, 'GET')
            if (status === 200) {                             
                _updateContext(true, data.currentBalance);
                setBalance(data as Balance);              
            }
        }
        if (!state.isRefresh) {
            _getBalance()           
        };
        setShowSpinner(false)
    }, [state.isRefresh, _updateContext])

    const _closeDialog = () => {
        setShowDialog(false);
    };

    const _updateBalance = () => {
        _updateContext(false, balance.currentBalance);
    }


    const dialogProps: ITransactionProps = {
        show: showDialog,
        closeDialog: _closeDialog,
        balance: balance.currentBalance
    }


    return (
        <div>
            {showSpinner ? <CircularProgress /> : null }
            <Card className={props.classes.root} variant="outlined">
                <CardContent>
                    <Typography className={props.classes.title} color="textSecondary" gutterBottom>
                        About user
                    </Typography>
                    <Typography variant="h5" component="h2">
                        Name: {balance.userName}
                        <p />
                        Email: {balance.userEmail}
                     </Typography>
                    <Typography className={props.classes.pos} color="textSecondary">
                        Balance
                    </Typography>
                    <Typography variant="h5" component="p">
                        {balance.currentBalance}
                    </Typography>
                </CardContent>
                <CardActions>
                    <Button size="small" color="primary" onClick={()=>setShowDialog(true)}>
                        Create transaction
                    </Button>
                    <Button size="small" color="primary" onClick={_updateBalance}>
                        Update balance
                    </Button>
                </CardActions>
            </Card>
            <TransactionDialog {...dialogProps} />
        </div>
    )
};
export default UserCard;