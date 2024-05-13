export const sendRequest = async (url: string, body: any) => {
    try {
        const response = await fetch(url, {
            method: "POST",
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

export const sendRequestWithAccess = async (method: string, url: string, body: any, accessToken: any) => {
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


export const sendRequestWithAccessWithId = async (method: string, url: string, body: any, accessToken: any, id: string) => {
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


