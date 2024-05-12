export const sendRequest = async (method: string, url: string, body: any) => {
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