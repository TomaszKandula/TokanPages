import * as React from "react";
import userAccountStyle from "./Styles/userAccountStyle";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{
    isLoading: boolean;
}

const UserAccountView = (props: IBinding): JSX.Element => 
{
    console.log(props);
    const classes = userAccountStyle();
    return(
        <div className={classes.section}>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
            <p>Layout goes here...</p>
        </div>
    );
}

export default UserAccountView;
