import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import SignupForm from "../Components/Account/signupForm";
import Footer from "../Components/Layout/footer";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/getFooterContentAction";
import { ActionCreators as SignupFormContent } from "../Redux/Actions/getSignupFormContentAction";

export default function SignupPage() 
{
    const dispatch = useDispatch();
    
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const signupForm = useSelector((state: IApplicationState) => state.getSignupFormContent);

    const getContent = React.useCallback(() =>
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(SignupFormContent.getSignupFormContent());
    }, [ dispatch ]);

    React.useEffect(() => getContent(), [ getContent ]);

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
