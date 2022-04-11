export interface IUnsubscribeContentDto
{
    content: 
    {
        contentPre: IContent,
        contentPost: IContent
    };
}

export interface IContent
{
    caption: string;
    text1: string;
    text2: string;
    text3: string;
    button: string;
}
