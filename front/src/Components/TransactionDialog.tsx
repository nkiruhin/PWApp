import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogTitle from '@material-ui/core/DialogTitle';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import TextField from '@material-ui/core/TextField';
import DialogActions from '@material-ui/core/DialogActions';
import Autocomplete, { RenderInputParams } from '@material-ui/lab/Autocomplete'
import AccountCircle from '@material-ui/icons/AccountCircle';
import { debounce } from '@material-ui/core';
import fetchApi from '../Service/fetcher';
import Alert from '@material-ui/lab/Alert/Alert';
import { AppContext } from './Context/AppContext';

export class Template {
    id: number = 0;
};

export interface ITransactionProps {
    show: boolean;
    template?: Template;
    closeDialog(): void;
    balance: number;
};


class Option
{
    id: string = '';
    email: string = '';
}


export const TransactionDialog: React.FC<ITransactionProps> = ({ show, closeDialog, template }) => {
    
    const url = "api/Transaction/";
    const searchUrl = "api/User/";

    const { state, dispatch } = React.useContext(AppContext);

    const [options, setOptions] = React.useState<Option[]>([]);
    const [user, setUser] = React.useState<Option | null>(null);
    const [amount, setAmount] = React.useState<number>(0);
    const [loading, setLoading] = React.useState<boolean>(false);
    const [alert, setAlert] = React.useState({ error: false, message: '' });

    const _onChange = (event: any, newValue: Option | null) => {       
        setUser(newValue);
    }

    const loadOptions = async (term: string) => {
        let { status, data } = await fetchApi(null, "api/User/", 'GET')
        setLoading(true);
        if (status === 200) {
            setOptions(data as Option[]);           
        }
        else
        {
            setAlert({ error: true, message: data.error })
            setLoading(false);
        }
    }

    React.useEffect(() => {
        const getTransaction = async () => {
            let { status, data } = await fetchApi(null, url + template?.id, 'GET')
            if (status === 200) {
                setAmount(data.amount);
                setUser(data as Option)
                setOptions([data as Option])
            } else {
                setAlert({ error: true, message: 'Error while getting transaction information' })
            }
        }
        if (template != null) {
            getTransaction();
        }
    }, [template])

    const _inputChange = async (event: any, newInputValue: string) => {
        let { status, data } = await fetchApi(null, searchUrl + '?term=' + newInputValue, 'GET')
        setLoading(true);
        if (status === 200) {
            setOptions(data as Option[]);
            setLoading(false);
        } else {
            setAlert({ error: true, message: 'Error while getting data' })
        }
    }

    const _sendTransaction = async (evn:any) => {
        evn.preventDefault();

        if (+amount === 0) {
            setAlert({ error: true, message: 'Cancel! Sum mast been above 0' })
            return;
        }

        if (+amount  > state.balance ) {
            setAlert({ error: true, message: 'Cancel! Sum of transaction is greater of balance' })
            return;
        }

        let params = `recipientId=${user?.id}&amount=${amount}`

        let { status, data } = await fetchApi(null, url, "POST", params)
        if (status !== 201) {
            setAlert({ error: true, message: data.title })
            return;
        }
        dispatch({ type: "REFRESH", payload: { ...state, isRefresh: false } });
        closeDialog();
    }

    const _label = (option: Option) => {
        return (
            <>
                <AccountCircle color='primary' />{ option.email }
            </>
            )
    }
    const _renderInput = (params: RenderInputParams) => {
        return (
            <TextField
                {...params}
                autoFocus
                required
                margin="dense"
                id="email"
                label="Email Address"
                type="email"
                fullWidth
                />
            )
    }

    return (
        <div>
            <Dialog open={show} onClose={closeDialog} aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">New transaction</DialogTitle>
                <form onSubmit={_sendTransaction }>
                <DialogContent>
                    <DialogContentText>
                        For create transaction select from list recipient email, and enter sum
                    </DialogContentText>
                    
                    <Autocomplete
                        id="email-combo-box"
                        value={user}
                        options={options}
                        onChange={_onChange}
                        onOpen={() => loadOptions("")}
                        onInputChange={ debounce(_inputChange, 500) }
                        getOptionSelected={(option, value) => option.id === value.id}
                        getOptionLabel={(option: Option) => option.email}
                        renderOption={_label}
                        renderInput={_renderInput}
                        loading={loading}
                    />
                    <TextField
                        margin="dense"
                        id="ammount"
                        required
                        label="Ammount"
                        type="number"
                        onChange={(evn: any) => setAmount(evn.target.value as number)}
                        value={amount}
                        fullWidth
                    />
                </DialogContent>
                {!!alert.error ? <Alert severity="error">{alert.message}</Alert> : null}
                <DialogActions>
                    <Button onClick={closeDialog} color="secondary">
                        Cancel
                    </Button>
                    <Button  color="primary" type="submit">
                        Send
                    </Button>
                    </DialogActions>
                  </form>
            </Dialog>
        </div>
    );
}
export default { TransactionDialog };
