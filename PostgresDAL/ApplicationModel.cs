
using System;
using System.Data;
using Npgsql;

public class ApplicationModel
{
    public static NpgsqlConnection dbcon;
    public static IDbCommand dbcmd;
    public static IDataReader reader;

    public static void Open()
    {
        string connectionString =
            "Server=localhost;" +
            "Database=test;" +
            "User ID=postgres;" +
            "Password=postgres;";
        dbcon = new NpgsqlConnection(connectionString);
        dbcon.Open();
        dbcmd = dbcon.CreateCommand();
    }
    
    public static void Close()
    {
        dbcon.Close();
    }

    public static DataSet GetClients()
    {
        NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter("SELECT * FROM clients ORDER BY pkey ASC;", dbcon);
        DataSet ds = new DataSet();
        myDataAdapter.Fill(ds, "Clients");
        return ds;
    }
    
    public static DataSet GetProduits()
    {
        
        NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter("SELECT * FROM produits ORDER BY pkey ASC;", dbcon);
        DataSet ds = new DataSet();
        myDataAdapter.Fill(ds, "Produits");
        return ds;
    }
    
    public static DataSet GetCommandes()
    {
        NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter("SELECT * FROM commandes ORDER BY pkey ASC;", dbcon);
        DataSet ds = new DataSet();
        myDataAdapter.Fill(ds, "Commandes");
        return ds;
    }
    
    public static DataSet GetProduitCommande()
    {
        NpgsqlDataAdapter myDataAdapter = new NpgsqlDataAdapter("SELECT * FROM produit_commande ORDER BY commande_pkey ASC, produit_pkey ASC;", dbcon);
        DataSet ds = new DataSet();
        myDataAdapter.Fill(ds, "Produit commande");
        return ds;
    }
    
    public static void UpdateClient(String id,String nom,String prenom)
    {
        NpgsqlCommand command = new NpgsqlCommand("UPDATE clients SET nom= :nom, prenom= :prenom WHERE pkey= :id;", dbcon);
        command.Parameters.Add(new NpgsqlParameter("id", DbType.Int32));
        command.Parameters.Add(new NpgsqlParameter("nom", DbType.String));
        command.Parameters.Add(new NpgsqlParameter("prenom", DbType.String));
        command.Parameters[0].Value = id;
        command.Parameters[1].Value = nom;
        command.Parameters[2].Value = prenom;
        reader = command.ExecuteReader();
        reader.Close();
    }
    
    public static void DeleteClient(String id)
    {
        NpgsqlCommand command = new NpgsqlCommand("DELETE FROM clients WHERE pkey= :id", dbcon);
        command.Parameters.Add(new NpgsqlParameter("id", DbType.Int32));
        command.Parameters[0].Value = id;
        reader = command.ExecuteReader();
        reader.Close();
    }
    
    public static void AddClient(String nom, String prenom)
    {
        NpgsqlCommand command = new NpgsqlCommand("INSERT INTO clients (nom,prenom) VALUES (:nom, :prenom);", dbcon);
        command.Parameters.Add(new NpgsqlParameter("nom", DbType.String));
        command.Parameters.Add(new NpgsqlParameter("prenom", DbType.String));
        command.Parameters[0].Value = nom;
        command.Parameters[1].Value = prenom;
        dbcmd = dbcon.CreateCommand();
        reader = command.ExecuteReader();
        reader.Close();
    }
    
    public static void UpdateProduit(String id, String label, String prix)
    {
        NpgsqlCommand command = new NpgsqlCommand("UPDATE produits SET label= :label, prix= :prix WHERE pkey= :id;", dbcon);
        command.Parameters.Add(new NpgsqlParameter("id", DbType.Int32));
        command.Parameters.Add(new NpgsqlParameter("label", DbType.String));
        command.Parameters.Add(new NpgsqlParameter("prix", DbType.Double));
        command.Parameters[0].Value = id;
        command.Parameters[1].Value = label;
        command.Parameters[2].Value = prix;
        reader = command.ExecuteReader();
        reader.Close();
    }
    
    public static void DeleteProduit(String id)
    {
        NpgsqlCommand command = new NpgsqlCommand("DELETE FROM produits WHERE pkey= :id", dbcon);
        command.Parameters.Add(new NpgsqlParameter("id", DbType.Int32));
        command.Parameters[0].Value = id;
        reader = command.ExecuteReader();
        reader.Close();
    }
    
    public static void AddProduit(String label,String prix)
    {
        NpgsqlCommand command = new NpgsqlCommand("INSERT INTO produits (label,prix) VALUES (:label, :prix);", dbcon);
        command.Parameters.Add(new NpgsqlParameter("label", DbType.String));
        command.Parameters.Add(new NpgsqlParameter("prix", DbType.String));
        command.Parameters[0].Value = label;
        command.Parameters[1].Value = prix;
        dbcmd = dbcon.CreateCommand();
        reader = command.ExecuteReader();
        reader.Close();
    }
    
    public static void UpdateCommande(String id,String client_pkey,String total)
    {
        NpgsqlCommand command = new NpgsqlCommand("UPDATE commandes SET client_pkey= :client_pkey, total= :total WHERE pkey= :id;", dbcon);
        command.Parameters.Add(new NpgsqlParameter("id", DbType.Int32));
        command.Parameters.Add(new NpgsqlParameter("client_pkey", DbType.String));
        command.Parameters.Add(new NpgsqlParameter("prix", DbType.Double));
        command.Parameters[0].Value = id;
        command.Parameters[1].Value = client_pkey;
        command.Parameters[2].Value = total;
        reader = command.ExecuteReader();
        reader.Close();
    }
    
    public static void DeleteCommande(String id)
    {
        NpgsqlCommand command = new NpgsqlCommand("DELETE FROM commandes WHERE pkey= :id", dbcon);
        command.Parameters.Add(new NpgsqlParameter("id", DbType.Int32));
        command.Parameters[0].Value = id;
        reader = command.ExecuteReader();
        reader.Close();
    }
    
    public static void AddCommande(String client_pkey)
    {
        NpgsqlCommand command = new NpgsqlCommand("INSERT INTO commandes (client_pkey,total) VALUES (:client_pkey, 0);", dbcon);
        command.Parameters.Add(new NpgsqlParameter("client_pkey", DbType.String));
        command.Parameters[0].Value = client_pkey;
        reader = command.ExecuteReader();
        reader.Close();
    }
    
    public static void UpdateProduitCommande(String commande_pkey,String produit_pkey,String quantite)
    {
        NpgsqlCommand command = new NpgsqlCommand("UPDATE produit_commande SET quantite= :quantite WHERE commande_pkey= :commande_pkey AND produit_pkey= :produit_pkey;", dbcon);
        command.Parameters.Add(new NpgsqlParameter("commande_pkey", DbType.Int32));
        command.Parameters.Add(new NpgsqlParameter("produit_pkey", DbType.Int32));
        command.Parameters.Add(new NpgsqlParameter("quantite", DbType.Double));
        command.Parameters[0].Value = commande_pkey;
        command.Parameters[1].Value = produit_pkey;
        command.Parameters[2].Value = quantite;
        reader = command.ExecuteReader();
        reader.Close();
    }
    
    public static void DeleteProduitCommande(String commande_pkey,String produit_pkey)
    {
        NpgsqlCommand command = new NpgsqlCommand("DELETE FROM produit_commande WHERE commande_pkey= :commande_pkey AND produit_pkey= :produit_pkey", dbcon);
        command.Parameters.Add(new NpgsqlParameter("commande_pkey", DbType.Int32));
        command.Parameters.Add(new NpgsqlParameter("produit_pkey", DbType.Int32));
        command.Parameters[0].Value = commande_pkey;
        command.Parameters[1].Value = produit_pkey;
        reader = command.ExecuteReader();
        reader.Close();
    }
    
    public static void AddProduitCommande(String commande_pkey,String produit_pkey,String quantite)
    {
        NpgsqlCommand command = new NpgsqlCommand("INSERT INTO produit_commande (commande_pkey,produit_pkey,quantite) VALUES (:commande_pkey, :produit_pkey, :quantite);", dbcon);
        command.Parameters.Add(new NpgsqlParameter("commande_pkey", DbType.Int32));
        command.Parameters.Add(new NpgsqlParameter("produit_pkey", DbType.Int32));
        command.Parameters.Add(new NpgsqlParameter("quantite", DbType.Int32));
        command.Parameters[0].Value = commande_pkey;
        command.Parameters[1].Value = produit_pkey;
        command.Parameters[2].Value = quantite;
        dbcmd = dbcon.CreateCommand();
        reader = command.ExecuteReader();
        reader.Close();
    }
}