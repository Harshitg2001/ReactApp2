﻿using Microsoft.Data.SqlClient;
using ReactApp2.Server.Models;
using System.Data;

namespace ReactApp2.Server.Respository
{
    public class CustomerDataAccess
    {
        private readonly string _connectionString;

        public CustomerDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Customer> GetAllClients()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetAllClientsWithRiskType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // ExecuteReader since it's a SELECT operation
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new Customer
                            {
                                CustomerID = (int)reader["CustomerID"],
                                UserID = (int)reader["UserID"],
                                RiskType = reader["RiskType"].ToString() // New line
                            });
                        }
                    }
                }
            }

            return customers;
        }

        public AdvisorPlan GetAdvisorPlanByCustomer(int customerId)
        {
            AdvisorPlan advisorPlan = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetAdvisorPlanByCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    // ExecuteReader since it's a SELECT operation
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            advisorPlan = new AdvisorPlan
                            {
                                PortfolioID = (int)reader["PortfolioID"],
                                AdvisorResponse = reader["AdvisorResponse"].ToString(),
                                Approval = (int)reader["Approval"]
                            };
                        }
                    }
                }
            }

            return advisorPlan;
        }

        public void UpdateAdvisorPlanApproval(int customerId, int approval)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UpdateAdvisorPlanApproval", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    command.Parameters.AddWithValue("@Approval", approval);

                    // ExecuteNonQuery since it's an UPDATE operation
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Investment> GetInvestmentsByCustomer(int customerId)
        {
            List<Investment> investments = new List<Investment>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetInvestmentsByCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    // ExecuteReader since it's a SELECT operation
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            investments.Add(new Investment
                            {
                                InvestmentId = (int)reader["InvestmentId"],
                                PortfolioId = (int)reader["PortfolioId"],
                                AssetId = (int)reader["AssetId"],
                                PurchasePrice = (int)reader["PurchasePrice"],
                                Quantity = (int)reader["Quantity"]
                            });
                        }
                    }
                }
            }

            return investments;
        }


        public void RegisterClient(User client)
        {

            // Validate password length and complexity


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("InsertCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters


                    command.Parameters.AddWithValue("@FirstName", client.FirstName);
                    command.Parameters.AddWithValue("@LastName", client.LastName);
                    command.Parameters.AddWithValue("@Email", client.Email);
                    command.Parameters.AddWithValue("@Password", client.Password);
                    command.Parameters.AddWithValue("@UserType", client.UserType);

                    // ExecuteNonQuery since it's an INSERT operation
                    command.ExecuteNonQuery();
                }
            }
        }

        public Customer ValidateClient(User client)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ValidateClient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@Email", client.Email);
                    command.Parameters.AddWithValue("@Password", client.Password);

                    // ExecuteReader since it's a SELECT operation
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerID = (int)reader["CustomerID"],
                                UserID = (int)reader["UserID"]
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public Customer GetClientByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetClientByEmail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@Email", email);

                    // ExecuteReader since it's a SELECT operation
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerID = (int)reader["CustomerID"],
                                UserID = (int)reader["UserID"]
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public void UpdateRiskType(int customerId, string riskType)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UpdateRiskType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    command.Parameters.AddWithValue("@RiskType", riskType);

                    // ExecuteNonQuery since it's an UPDATE operation
                    command.ExecuteNonQuery();
                }
            }
        }


        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DeleteCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    // ExecuteNonQuery since it's a DELETE operation
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Portfolio> GetCustomersByCustomerID(int customerId)
        {
            var portfolios = new List<Portfolio>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetCustomersByCustomerID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parameters
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    // ExecuteReader since it's a SELECT operation
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            portfolios.Add(new Portfolio
                            {
                                CustomerID = (int)reader["CustomerID"],
                                AdvisorID = (int)reader["AdvisorID"],
                                PortfolioName = reader["PortfolioName"].ToString(),
                                RiskType = reader["RiskType"].ToString(),
                                CurrentValue = (int)reader["CurrentValue"],
                                //TotalInvestedValue = (int)reader["TotalInvestedValue"]
                            });
                        }
                    }
                }
            }

            return portfolios;
        }

    }
}
