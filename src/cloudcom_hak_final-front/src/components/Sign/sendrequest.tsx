const sendRequest = async (method: string, url: string, body: any) => {
    try {
        const response = await fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body)
        });
        
        if (response.ok) {
            return await response.json();
        } else {
            throw new Error('Ошибка при выполнении запроса');
        }
    } catch (error) {
        console.error('Ошибка:', error);
        throw new Error('Произошла ошибка при обращении к серверу');
    }
};

 const sendRequest = async (method: string, url: string, body: any, accessToken?: any) => {
    try {
        const response = await fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json',
                "Authorization": `Bearer ${accessToken}`
            },
            body: JSON.stringify(body)
        });
        
        if (response.ok) {
            return await response.json();
        } else {
            throw new Error('Ошибка при выполнении запроса');
        }
    } catch (error) {
        console.error('Ошибка:', error);
        throw new Error('Произошла ошибка при обращении к серверу');
    }
};


const sendRequestWithAccessWithId = async (method: string, url: string, body: any, accessToken: any, id: string) => {
    try {
        const response = await fetch(url, {
            method: method,
            headers: {
                'departmentId': id,   
                'Content-Type': 'application/json',
                "Authorization": `Bearer ${accessToken}`
            },
            body: JSON.stringify(body)
        });
        
        if (response.ok) {
            return await response.json();
        } else {
            throw new Error('Ошибка при выполнении запроса');
        }
    } catch (error) {
        console.error('Ошибка:', error);
        throw new Error('Произошла ошибка при обращении к серверу');
    }
};


export const sendGetRequest = async (url: string, accessToken: any) => {
    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                "Authorization": `Bearer ${accessToken}`
            }
        });
        
        if (response.ok) {
            return await response.json();
        } else {
            throw new Error('Ошибка при выполнении запроса');
        }
    } catch (error) {
        console.error('Ошибка:', error);
        throw new Error('Произошла ошибка при обращении к серверу');
    }
};

export const sendGetRequestId = async (url: string, accessToken: any, id: string) => {
    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                "departmentId": id,
                "Authorization": `Bearer ${accessToken}`
            }
        });
        
        if (response.ok) {
            return await response.json();
        } else {
            throw new Error('Ошибка при выполнении запроса');
        }
    } catch (error) {
        console.error('Ошибка:', error);
        throw new Error('Произошла ошибка при обращении к серверу');
    }
};