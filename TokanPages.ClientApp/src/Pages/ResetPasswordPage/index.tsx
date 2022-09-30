import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { ResetPassword } from "../../Components/Account";

import { 
    GetNavigationContentAction, 
    GetFooterContentAction, 
    GetResetPasswordContentAction 
} from "../../Store/Actions";

export const ResetPasswordPage = (): JSX.Element => 
{
    const dispatch = useDispatch();
    
    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const resetForm = useSelector((state: IApplicationState) => state.getResetPasswordContent);

    React.useEffect(() => 
    {
        dispatch(GetNavigationContentAction.get());
        dispatch(GetFooterContentAction.get());
        dispatch(GetResetPasswordContentAction.get());
    }, 
    [ dispatch, language?.id ]);
    
    return (
        <>     
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <ResetPassword content={resetForm?.content} isLoading={resetForm?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
