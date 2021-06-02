export interface IUnsubscribeContentDto
{
    content: 
    {
        contentPre:
        {
            caption: string;
            text1: string;
            text2: string;
            text3: string;
            button: string;
        },
        contentPost:
        {
            caption: string;
            text1: string;
            text2: string;
            text3: string;
            button: string;
        }
    };
}
