import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import NavigationView from "../Components/Layout/navigationView";
import SigninFormView from "../Components/Account/signinFormView";
import Footer from "../Components/Layout/footer";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/getFooterContentAction";
import { ActionCreators as SigninFormContent } from "../Redux/Actions/getSigninFormContentAction";

export default function SigninPage() 
{  
    const dispatch = useDispatch();
    
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const signinForm = useSelector((state: IApplicationState) => state.getSigninFormContent);

    const getContent = React.useCallback(() =>
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(SigninFormContent.getSigninFormContent());
    }, [ dispatch ]);

    React.useEffect(() => getContent(), [ getContent ]);
    
    return (
        <>
            <NavigationView content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <SigninFormView content={signinForm?.content} isLoading={signinForm?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
