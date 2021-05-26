import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import SigninForm from "../Components/Account/signinForm";
import Footer from "../Components/Layout/footer";
import { IApplicationState } from "../Redux/applicationState";
import { combinedDefaults } from "../Redux/combinedDefaults";
import { ActionCreators as NavigationContent } from "../Redux/Actions/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/getFooterContentAction";
import { ActionCreators as SigninFormContent } from "../Redux/Actions/getSigninFormContentAction";

export default function SigninPage() 
{  
    const dispatch = useDispatch();
    
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const signinForm = useSelector((state: IApplicationState) => state.getSigninFormContent);

    const fetchNavigationContent = React.useCallback(() => dispatch(NavigationContent.getNavigationContent()), [ dispatch ]);
    const fetchFooterContent = React.useCallback(() => dispatch(FooterContent.getFooterContent()), [ dispatch ]);
    const fetchSigninFormContent = React.useCallback(() => dispatch(SigninFormContent.getSigninFormContent()), [ dispatch ]);

    React.useEffect(() => 
    { 
        if (navigation?.content === combinedDefaults.getNavigationContent.content) 
            fetchNavigationContent(); 
    }, [ fetchNavigationContent, navigation?.content ]);
    
    React.useEffect(() => 
    { 
        if (footer?.content === combinedDefaults.getFooterContent.content) 
            fetchFooterContent(); 
    }, [ fetchFooterContent, footer?.content ]);
    
    React.useEffect(() => 
    { 
        if (signinForm?.content === combinedDefaults.getSigninFormContent.content) 
            fetchSigninFormContent(); 
    }, [ fetchSigninFormContent, signinForm?.content ]);
    
    return (
        <>
            <Navigation navigation={navigation} isLoading={navigation?.isLoading} />
            <Container>
                <SigninForm signinForm={signinForm} isLoading={signinForm?.isLoading} />
            </Container>
            <Footer footer={footer} isLoading={footer?.isLoading} />
        </>
    );
}
