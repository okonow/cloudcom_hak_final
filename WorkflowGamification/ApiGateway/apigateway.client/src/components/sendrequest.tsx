export const sendRefreshTokenRequest = async () => {
    try {
        const response = await fetch("https://localhost:7288/UserApi/Token/RefreshToken", {
            method: "PUT",
            headers: {
                'Content-Type': 'application/json',
            },
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


export const sendPostRequest = async (url: string, body: any) => {
    try {
        const response = await fetch(url, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include',
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


export const sendPostRequestWithAccess = async (url: string, body: any, accessToken: any) => {
    try {
        const response = await fetch(url, {
            method: "POST",
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

 export const sendPatchRequest = async (url: string, body: any, accessToken: any) => {
    try {
        const response = await fetch(url, {
            method: "PATCH",
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


const sendPatchRequestWithAccessWithId = async (url: string, body: any, accessToken: any, id: string) => {
    try {
        const response = await fetch(url, {
            method: "PATCH",
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

export const sendPutRequest = async (url: string, body: any, accessToken: any) => {
    try {
        const response = await fetch(url, {
            method: "PUT",
            credentials: 'include',
            headers: {  
                'Content-Type': 'application/json',
                "Authorization": `Bearer ${accessToken}`
            },
            body: JSON.stringify(body)
        });
        
        if (response.ok) {
            return response;
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
            credentials: 'include',
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

export const sendGetRequestWithId = async (url: string, accessToken: any, id: string) => {
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