using QuickBooksSharp.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace QuickBooksSharp
{
    public interface IDataService
    {
        Task<IntuitResponse<BatchItemResponse[]>> BatchAsync(IntuitBatchRequest r);
        Task<IntuitResponse<IntuitEntity>> GetAsync(string id, Type entityType);
        Task<IntuitResponse<TEntity>> GetAsync<TEntity>(string id) where TEntity : IntuitEntity;
        Task<IntuitResponse<CDCResponse>> GetCDCAsync(DateTimeOffset changedSince, IEnumerable<string> entityNames);
        Task<Report> GetReportAsync(string reportName, Dictionary<string, string> parameters);

        /// <summary>
        /// Create, Update, or SparseUpdate the entity, depending on the value of the 'sparse' property
        /// </summary>
        /// <typeparam name="TEntity">QuickBooks Entity</typeparam>
        /// <param name="e">Entity to be sent</param>
        /// <param name="operation">Defines the operation to be executed in QuickBooks.</param>
        /// <param name="include">Defines the "include" query parameter. For example: Required when voiding a payment</param>
        /// <returns></returns>
        Task<IntuitResponse<TEntity>> PostAsync<TEntity>(TEntity e, OperationEnum? operation = null, OperationEnum? include = null) where TEntity : IntuitEntity;

        Task<IntuitResponse<QueryResponse<TEntity>>> QueryAsync<TEntity>(string query) where TEntity : IntuitEntity;

        /// <summary>
        /// Get an invoice as PDF
        /// <para>This resource returns the specified object in the response body as an Adobe Portable Document Format (PDF) file. The resulting PDF file is formatted according to custom form styles in the company settings.</para>
        /// <see href="https://developer.intuit.com/app/developer/qbo/docs/api/accounting/most-commonly-used/invoice#get-an-invoice-as-pdf">QBO Documentation</see>
        /// </summary>
        /// <param name="invoiceId">Unique identifier for this object</param>
        /// <returns>This resource returns the specified object in the response body as an Adobe Portable Document Format (PDF) file. The resulting PDF file is formatted according to custom form styles in the company settings.</returns>
        Task<Stream> GetInvoicePDFAsync(string invoiceId);
        
        /// <summary>
        /// Sends an invoice to a customer or vendor. This will update the Invoice.EmailStatus to EmailSent and Invoice.DeliveryInfo
        /// will be populated with the sending information.
        /// </summary>
        /// <param name="invoiceId">Id of the invoice to send</param>
        /// <param name="sendTo">Email address to send the invoice to. Optional. If not provided, the Invoice.BillEmail address will be used.
        /// If provided, this will replace the Invoice.BillEmail with this value</param>
        /// <returns>The updated invoice after sending information</returns>
        Task<IntuitResponse<Invoice>> SendInvoice(string invoiceId, string? sendTo = null);

        Task<TaxService> PostTaxServiceAsync(TaxService taxService);
    }
}