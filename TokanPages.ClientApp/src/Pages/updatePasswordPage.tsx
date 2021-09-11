import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import UpdatePassword from "../Components/Account/updatePassword";
import Footer from "../Components/Layout/footer";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/Content/getFooterContentAction";
import { ActionCreators as UpdateFormContent } from "../Redux/Actions/Content/getUpdatePasswordContentAction";

const UpdatePasswordPage = (): JSX.Element => 
{
    const dispatch = useDispatch();
    
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const updateForm = useSelector((state: IApplicationState) => state.getUpdatePasswordContent);

    const getContent = React.useCallback(() =>
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(UpdateFormContent.getUpdatePasswordContent());
    }, [ dispatch ]);

    React.useEffect(() => getContent(), [ getContent ]);
    
    return (
        <>     
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <UpdatePassword content={updateForm?.content} isLoading={updateForm?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}

export default UpdatePasswordPage;
