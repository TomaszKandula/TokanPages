import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../../Redux/Actions/Content/getFooterContentAction";
import { ActionCreators as SigninFormContent } from "../../Redux/Actions/Content/getUserSigninContentAction";
import Navigation from "../../Components/Layout/navigation";
import UserSignin from "../../Components/Account/userSignin";
import Footer from "../../Components/Layout/footer";

const SigninPage = (): JSX.Element => 
{  
    const dispatch = useDispatch();
    
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const signinForm = useSelector((state: IApplicationState) => state.getUserSigninContent);

    React.useEffect(() => 
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(SigninFormContent.getUserSigninContent());
    }, 
    [ dispatch ]);
    
    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <UserSignin content={signinForm?.content} isLoading={signinForm?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}

export default SigninPage;
