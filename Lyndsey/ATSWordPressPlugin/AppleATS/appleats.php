<?php
/*
	Plugin Name: Apple ATS
	Description: Applicant Tracking System for Apple!
	Version: 1.0
	Author: Joe Fortunato
	License: GPL2
*/
defined( "ABSPATH" ) or die( "You're not WordPress!" );
require_once('globalconstants.php');
require_once( ABSPATH . 'wp-admin/includes/upgrade.php' );

register_activation_hook(__FILE__, 'appleats_Activate');
register_uninstall_hook( __FILE__, 'appleats_Uninstall');

/* Registers uninstall hook and creates SQL tables for this plugin */
function appleats_Activate()
{	 
	global $wpdb,$appleats_ApplicationTableName;
	$table_name = $wpdb->prefix . $appleats_ApplicationTableName;
	
	$charset_collate = $wpdb->get_charset_collate();

	$sql = "CREATE TABLE $table_name (
	  id mediumint(9) NOT NULL AUTO_INCREMENT,
	  name varchar(100) NOT NULL,
	  email varchar(100) NOT NULL,
	  createdon datetime DEFAULT CURRENT_TIMESTAMP NOT NULL,
	  PRIMARY KEY  (id)
	) $charset_collate;";
	
	dbDelta( $sql );
	appleats_InsertTestApplicationRecord();
}

/* Drops the SQL tables for this plugin */
function appleats_Uninstall()
{
	global $wpdb,$appleats_ApplicationTableName;
	$table_name = $wpdb->prefix . $appleats_ApplicationTableName;
	
	$sql = "DROP TABLE IF EXISTS $table_name;";

	$wpdb->query( $sql );
}

/* Insert some junk test data to verify table exists */
function appleats_InsertTestApplicationRecord()
{
	global $wpdb,$appleats_ApplicationTableName;
	$table_name = $wpdb->prefix . $appleats_ApplicationTableName;
	
	$wpdb->insert($table_name, 
		array(
			"name" => "test applicant",
			"email" => "test@testdomain.com"
		) 
	);
	
}

?>