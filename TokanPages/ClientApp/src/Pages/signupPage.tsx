import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import SignupForm from "../Components/Account/signupForm";
import Footer from "../Components/Layout/footer";
import { IApplicationState } from "../Redux/applicationState";
import { combinedDefaults } from "../Redux/combinedDefaults";
import { ActionCreators as NavigationContent } from "../Redux/Actions/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/getFooterContentAction";
import { ActionCreators as SignupFormContent } from "../Redux/Actions/getSignupFormContentAction";

export default function SignupPage() 
{
    const dispatch = useDispatch();
    
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const signupForm = useSelector((state: IApplicationState) => state.getSignupFormContent);

    const fetchNavigationContent = React.useCallback(() => dispatch(NavigationContent.getNavigationContent()), [ dispatch ]);
    const fetchFooterContent = React.useCallback(() => dispatch(FooterContent.getFooterContent()), [ dispatch ]);
    const fetchSignupFormContent = React.useCallback(() => dispatch(SignupFormContent.getSignupFormContent()), [ dispatch ]);

    React.useEffect(() => 
    { 
        if (navigation?.content === combinedDefaults.getNavigationContent.content) 
            fetchNavigationContent(); 
    }, [ fetchNavigationContent, navigation ]);

    React.useEffect(() => 
    { 
        if (footer.content === combinedDefaults.getFooterContent.content) 
            fetchFooterContent(); 
    }, [ fetchFooterContent, footer ]);

    React.useEffect(() => 
    { 
        if (signupForm?.content === combinedDefaults.getSignupFormContent.content) 
            fetchSignupFormContent(); 
    }, [ fetchSignupFormContent, signupForm ]);

    return (
        <>
            <Navigation navigation={navigation} isLoading={navigation?.isLoading} />
            <Container>
                <SignupForm signupForm={signupForm} isLoading={signupForm?.isLoading} />
            </Container>
            <Footer footer={footer} isLoading={footer?.isLoading} />
        </>
    );
}
