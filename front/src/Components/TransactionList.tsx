import React from 'react';
import MaterialTable from 'material-table'
import ArrowForward from '@material-ui/icons/ArrowForward';
import ArrowBack from '@material-ui/icons/ArrowBack';
import fetchApi from '../Service/fetcher';
import green from '@material-ui/core/colors/green';
import { TransactionDialog, ITransactionProps, Template } from './TransactionDialog';
import { AppContext } from './Context/AppContext';

enum Type { Debit, Credit }

class Collumn {
    title: string = "";
    field: string = "";
    type?: any;
    hidden?: boolean;
    render?(event: any, rowData: any): any;
    searchable?: boolean;
    sorting?: boolean;
    lookup?: any;
    customFilterAndSearch?: (term: Array<string>, rowData: IData) => boolean;
    defaultSort?: "desc" | "asc" | undefined;
    currencySetting?: { locale?: string, currencyCode?: string, minimumFractionDigits?: number, maximumFractionDigits?: number };
}

interface IData {
    id: number
    timestamp: Date;
    recipient: string;
    sender: string;
    amount: number;
    balance: number;
    type: Type;
}


const collumns: Collumn[] = [
    { title: 'Id', field: 'id', type: 'numeric', hidden: true },
    { title: 'Timestamp', field: 'timestamp', type: 'datetime', defaultSort : 'desc' },
    { title: 'Sender name', field: 'sender' },
    { title: 'Recipient name', field: 'recipient' },
    { title: 'Amount', field: 'amount', type: 'numeric' },
    { title: 'Balance', field: 'balance', type: 'numeric' },
    {
        title: 'Debit/Credit',
        field: 'type',
        lookup: { 'Credit': Type[Type.Credit], 'Debit': Type[Type.Debit] },
        customFilterAndSearch: (term, rowData) => {
            if (term.length === 0) return true;
            return term[0] === Type[rowData.type] || term[1] === Type[rowData.type]
        },
        render: (rowData: IData) => rowData.type === Type.Credit ? <ArrowForward color="secondary" /> : <ArrowBack style={{ color: green[500] }} />
    }
]
export class TrasactionListProps
{

}

export const TransactionList: React.FC<TrasactionListProps> = () => {

    const { state, } = React.useContext(AppContext);

    const [showDialog, setShowDialog] = React.useState(false);
    const [rows, setRows] = React.useState<IData[]>([]);
    const [loading, setLoading] = React.useState(false);
    const [template, setTemplate] = React.useState <Template>()

    React.useEffect(() => {
        setLoading(true)
        const getTransactions = async () => {
            const url = "api/Transaction/";
            const { data, status } = await fetchApi(null, url, 'GET');
            if (status === 200) {
                setRows(data as IData[]);
                setLoading(false)
            }
        }
        if (!state.isRefresh) getTransactions();
    }, [state.isRefresh])


    const _closeDialog = () => {
        setShowDialog(false);
    };

    const _reapeatTransaction = (event: any, rowData: IData[] | IData) => {
        if (!(rowData instanceof Array)) {
            if (rowData.type === Type.Credit) {
                console.log(rowData)
                setTemplate({ id: rowData.id })
            }
            setShowDialog(true); 
        } 
    }

    const dialogProps: ITransactionProps = {
        show: showDialog,  
        closeDialog: _closeDialog,
        template: template,
        balance: 0
    }

    return (
        <div>
            <MaterialTable
                columns = { collumns }
                data={ rows }
                isLoading= { loading }
                actions = {[
                    rowData =>({
                        icon: 'repeat',
                        tooltip: 'Repeat tranaction',
                        onClick: _reapeatTransaction,
                        hidden: rowData.type === Type.Debit
                    })
                ]}
                options={{
                    filtering: true
                }}
                title="Transaction history"
            />
            <TransactionDialog {...dialogProps} />
	    </div>
    );

}
    

export default TransactionList;