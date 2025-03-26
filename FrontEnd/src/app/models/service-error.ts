export interface ServiceError {
    errorMessage:string|undefined; 

    /// <summary>
    /// string key to use for internationalization
    /// </summary>
    errorKey:string|undefined;
   
    /// <summary>
    /// Optional description about the error
    /// </summary>
    description:string|undefined; 
}