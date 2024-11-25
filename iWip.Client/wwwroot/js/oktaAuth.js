/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

function initializeOkta(issuer, clientId, redirectUrl, responseType) {
    const oktaConfig = {
        clientId: clientId,
        issuer: issuer,
        redirectUri: redirectUrl,
        responseType: responseType,
    };

    window.oktaAuth = new OktaAuth(oktaConfig);
}

async function loginToOkta() {
    try {
        await window.oktaAuth.signInWithRedirect();
    } catch (error) {
        console.log('error', error);
    }
}

async function handleOktaCallback() {
    if (window.oktaAuth.token.isLoginRedirect()) {
        const { tokens } = await window.oktaAuth.token.parseFromUrl();
        window.oktaAuth.tokenManager.setTokens(tokens);

        return JSON.stringify(tokens);;
    }
}

async function logoutFromOkta() {
    try {
        await window.oktaAuth.signOut();
    } catch (error) {
        console.error('Error during logout:', error);
    }
}

async function checkAuthentication() {
    console.log('isauth', await window.oktaAuth.isAuthenticated())
    return await window.oktaAuth.isAuthenticated();
}