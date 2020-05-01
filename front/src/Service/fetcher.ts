export const fetchApi = async (callback: any, url: string, method: string, body?: any) => {
    let token = localStorage.Token
    let header = undefined
    let data;
    if (token !== null) {
        header = new Headers({
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/x-www-form-urlencoded'
        })
    }
    const res = await fetch(url, { method: method, body: body, headers: header });
    try {
        data = await res.json();
    } catch {
        data = {
            error: 'Неверный формат данных'
        }
        if (callback !== null) {
            callback(data, 500);
        }
        return { data: data, status: 500 }
    }
    if (callback !== null) {
        callback(data, res.status);
    }
    return { data: data, status: res.status }
}
export default fetchApi